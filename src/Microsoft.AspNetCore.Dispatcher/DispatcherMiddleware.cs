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
        private readonly IDispatcher[] _dispatchers;
        private readonly IEndpointSelector[] _selectors;

        public DispatcherMiddleware(RequestDelegate next, IDispatcher[] dispatchers, IEndpointSelector[] selectors)
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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var context = new DefaultDispatcherContext(httpContext);
            httpContext.Features.Set<IDispatcherFeature>(context);

            for (var i = 0; i < _dispatchers.Length; i++)
            {
                var dispatcher = _dispatchers[i];

                await dispatcher.InvokeAsync(httpContext, context);
                if (context.RequestDelegate != null)
                {
                    break;
                }
            }

            await _next(httpContext);
        }
    }
}
