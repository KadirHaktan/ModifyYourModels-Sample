
using Autodesk.Forge;
using Core.Interfaces.Adapters;
using Core.Interfaces.Services.Auth;
using Core.Utilities.Ioc;
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


            services.AddTransient<IAppBundleService, AutoDeskAppBundleService>();
            services.AddTransient<IAppBundleServiceAdapter, AppBundleServiceAdapter>();


            services.AddScoped<IAutoDeskDesignAutomationRepository,AutoDeskDesignAutomationRepository>();

            services.AddTransient<IEngineServices, AutoDeskEngineService>();
            services.AddTransient<IEngineServiceAdapter, EngineServiceAdapter>();

            services.AddTransient<IWorkItemService, AutoDeskWorkItemService>();
            services.AddTransient<IWorkItemServiceAdapter, WorkItemServiceAdapter>();


            services.AddTransient<IActivityService, AutoDeskActivityService>();
            services.AddTransient<IActivityServiceAdapter, ActivityServiceAdapter>();




        }
    }
}
