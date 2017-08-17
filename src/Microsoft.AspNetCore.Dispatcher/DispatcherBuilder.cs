// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Dispatcher;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    public abstract class DispatcherBuilder
    {
        public abstract IList<IDispatcher> Dispatchers { get; }

        public abstract IList<EndpointMapping> Endpoints { get; }

        public abstract IList<IEndpointSelector> Selectors { get; }
    }
}
