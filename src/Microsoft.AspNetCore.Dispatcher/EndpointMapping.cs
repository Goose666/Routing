// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Dispatcher
{
    public struct EndpointMapping
    {
        public EndpointMapping(EndpointDescriptor endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Endpoint = endpoint;
            Address = null;
        }

        public EndpointMapping(EndpointDescriptor endpoint, AddressDescriptor address)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Endpoint = endpoint;
            Address = address;
        }

        public EndpointDescriptor Endpoint { get; }

        public AddressDescriptor Address { get; }
    }
}
