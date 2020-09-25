using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.Forge;
using Autodesk.Forge.DesignAutomation;
using Core.Interfaces.Adapters;
using Core.Interfaces.Auth;
using Core.Utilities.Ioc;
using Entities.Concerete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Adapters;
using Services.Concerete;

namespace Services.DependencyResolvers
{
    public class ServiceModule : ICoreModules
    {
        
        public void Load(IServiceCollection services)
        {
            services.AddScoped<ITwoLeggedApi, TwoLeggedApi>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthServiceAdapter, AuthServiceAdapter>();

        }
    }
}
