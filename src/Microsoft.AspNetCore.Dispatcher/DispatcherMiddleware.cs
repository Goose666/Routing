// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Dispatcher
{
    internal sealed class DispatcherMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate[] _dispatchers;
        private readonly IEndpointSelector[] _selectors;

        public DispatcherMiddleware(RequestDelegate next, RequestDelegate[] dispatchers, IEndpointSelector[] selectors)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (dispatchers == null)
            {
                throw new ArgumentNullException(nameof(dispatchers));
            }

            if (selectors == null)
            {
                throw new ArgumentNullException(nameof(selectors));
            }

            _next = next;
            _dispatchers = dispatchers;
            _selectors = selectors;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var feature = new DispatcherFeature();
            context.Features.Set<IDispatcherFeature>(feature);

            for (var i = 0; i < _dispatchers.Length; i++)
            {
                var dispatcher = _dispatchers[i];

                await dispatcher.Invoke(context);
                if (feature.RequestDelegate != null)
                {
                    break;
                }
            }

            await _next(context);
        }
    }
}
