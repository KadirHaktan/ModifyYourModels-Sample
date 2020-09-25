using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Ioc
{
    public static class ServiceTool
    {
        public static IServiceProvider serviceProvider { get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            serviceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
