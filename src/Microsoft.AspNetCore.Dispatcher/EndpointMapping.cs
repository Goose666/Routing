// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Dispatcher
{
    public struct EndpointMapping
    {
        public EndpointMapping(DispatcherEndpoint endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Endpoint = endpoint;
            Address = null;
        }

        public EndpointMapping(DispatcherEndpoint endpoint, Address address)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Endpoint = endpoint;
            Address = address;
        }

        public DispatcherEndpoint Endpoint { get; }

        public Address Address { get; }
    }
}
