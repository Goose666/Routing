// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Dispatcher
{
    internal sealed class DefaultDispatcherBuilder : DispatcherBuilder
    {
        private readonly AddressTable _addressTable;

        public DefaultDispatcherBuilder(AddressTable addressTable)
        {
            if (addressTable == null)
            {
                throw new ArgumentNullException(nameof(addressTable));
            }

            _addressTable = addressTable;
        }

        public override IList<IDispatcher> Dispatchers { get; } = new List<IDispatcher>();

        public override IList<EndpointMapping> Endpoints { get; } = new List<EndpointMapping>();

        public override IList<IEndpointSelector> Selectors { get; } = new List<IEndpointSelector>();
    }
}
