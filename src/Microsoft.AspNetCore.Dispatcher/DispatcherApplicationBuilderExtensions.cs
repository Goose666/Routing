// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class DispatcherApplicationBuilderExtensions
    {
        private const string DispatcherBuilderKey = "DispatcherBuilder";

        public static DispatcherBuilder GetDispatcherBuilder(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Properties.TryGetValue(DispatcherBuilderKey, out var obj);
            return (DispatcherBuilder)obj;
        }

        public static void SetDispatcherBuilder(this IApplicationBuilder builder, DispatcherBuilder dispatcherBuilder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Properties[DispatcherBuilderKey] = dispatcherBuilder;
        }
    }
}
