using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Business.DependencyResolvers
{
    public class CoreModule : ICoreModules
    {
        public void Load(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddNewtonsoftJson();

        }
    }
}
