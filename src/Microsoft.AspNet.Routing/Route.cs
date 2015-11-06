﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Routing.Internal;

namespace Microsoft.AspNet.Routing
{
    public class Route : RouteBase
    {
        public Route(
            RouteSpec routeSpec,
            IRouteEndpoint target)
            : base(routeSpec)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            Target = target;
        }

        protected IRouteEndpoint Target { get; }

        public override Task OnRouteMatchedAsync(RouteContext context)
        {
            context.Handler = Target.CreateHandler(context.RouteData);
            return TaskCache.CompletedTask;
        }
    }
}