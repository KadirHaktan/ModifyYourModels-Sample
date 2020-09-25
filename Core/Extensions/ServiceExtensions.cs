using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,ICoreModules[] modules)
        {
            foreach (ICoreModules module in modules)
            {
                module.Load(services);
            }

            return ServiceTool.Create(services);
        }
    }
}
