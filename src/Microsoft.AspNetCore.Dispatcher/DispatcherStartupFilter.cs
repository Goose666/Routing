// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Dispatcher
{
    public sealed class DispatcherStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return (builder) =>
            {
                var addressTable = (AddressTable)builder.ApplicationServices.GetService(typeof(AddressTable));
                if (addressTable == null)
                {
                    next(builder);
                    return;
                }

                var dispatcherBuilder = new DefaultDispatcherBuilder(addressTable);
                builder.SetDispatcherBuilder(dispatcherBuilder);

                builder.Use((n) =>
                {
                    var dispatchers = dispatcherBuilder.Dispatchers.ToArray();
                    var selectors = dispatcherBuilder.Selectors.ToArray();

                    var middleware = new DispatcherMiddleware(n, dispatchers, selectors);
                    return middleware.InvokeAsync;
                });

                next(builder);

                builder.Use((n) => async (context) =>
                {
                    var appFunc = context.Features.Get<IDispatcherFeature>().RequestDelegate;
                    if (appFunc != null)
                    {
                        await appFunc(context);
                    }
                });
            };
        }
    }
}
