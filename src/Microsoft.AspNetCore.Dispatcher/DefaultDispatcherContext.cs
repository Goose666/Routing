// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Dispatcher
{
    public sealed class DefaultDispatcherContext : DispatcherContext
    {
        public DefaultDispatcherContext(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            HttpContext = httpContext;
        }

        public override HttpContext HttpContext { get; }

        public override EndpointDescriptor Endpoint { get; set; }

        public override RequestDelegate RequestDelegate { get; set; }
    }
}
