
using Autodesk.Forge;
using Core.Interfaces.Adapters;
using Core.Interfaces.Services.Auth;
using Core.Utilities.Ioc;
using Entities.Concerete;
using Microsoft.Extensions.DependencyInjection;
using Repository.Abstract;
using Repository.Concerete;
using Services.Abstract;
using Services.Adapters;
using Services.Adapters.Abstract;
using Services.Adapters.Concerete;
using Services.Concerete;

namespace Services.DependencyResolvers
{
    public class ServiceModule : ICoreModules
    {
        
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<ITwoLeggedApi, TwoLeggedApi>();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IAuthServiceAdapter, AuthServiceAdapter>();


            services.AddTransient<IAppBundleService<AutoDeskAppBundle>, AutoDeskAppBundleService>();
            services.AddTransient<IAppBundleServiceAdapter<AutoDeskAppBundle>, AppBundleServiceAdapter<AutoDeskAppBundle>>();


            services.AddScoped<IAutoDeskAppBundleRepository, AutoDeskAppBundleRepository>();

            services.AddTransient<IEngineServices, AutoDeskEngineService>();
            services.AddTransient<IEngineServiceAdapter, EngineServiceAdapter>();



        }
    }
}
