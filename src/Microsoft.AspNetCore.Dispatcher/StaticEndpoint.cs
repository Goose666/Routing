// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Dispatcher
{
    public sealed class StaticEndpoint : DispatcherEndpoint
    {
        public StaticEndpoint(RequestDelegate requestDelegate, string displayName, IEndpointSelectorMetadata[] selectorMetadata)
        {
            if (requestDelegate == null)
            {
                throw new ArgumentNullException(nameof(requestDelegate));
            }

            RequestDelegate = requestDelegate;
            DisplayName = displayName;
            SelectorMetadata = selectorMetadata ?? Array.Empty<IEndpointSelectorMetadata>();
        }

        public override string DisplayName { get; }

        public RequestDelegate RequestDelegate { get; }

        public override IReadOnlyList<IEndpointSelectorMetadata> SelectorMetadata { get; }
    }
}
