// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace RoutingSample.Web
{
    public class Program
    {
        private static readonly TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(10);

        public static void Main(string[] args)
        {
            var webHost = GetWebHostBuilder().Build();
            webHost.Run();
        }

        // For unit testing
        public static IWebHostBuilder GetWebHostBuilder()
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<Startup>();
        }
    }
}
