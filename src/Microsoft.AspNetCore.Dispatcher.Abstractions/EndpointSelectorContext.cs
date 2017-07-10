// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Dispatcher
{
    public sealed class EndpointSelectorContext
    {
        public EndpointSelectorContext(
            HttpContext httpContext,
            IReadOnlyList<DispatcherEndpoint> endpoints,
            IReadOnlyList<IEndpointSelector> selectors)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (endpoints == null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            if (selectors == null)
            {
                throw new ArgumentNullException(nameof(selectors));
            }

            HttpContext = httpContext;
            Endpoints = new List<DispatcherEndpoint>(endpoints);
            Selectors = selectors;
        }

        public IList<DispatcherEndpoint> Endpoints { get; }

        public HttpContext HttpContext { get; }

        public RequestDelegate RequestDelegate { get; set; }

        public IReadOnlyList<IEndpointSelector> Selectors { get; }
    }
}
