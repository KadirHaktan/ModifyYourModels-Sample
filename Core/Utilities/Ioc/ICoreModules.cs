using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Ioc
{
    public interface ICoreModules
    {
        void Load(IServiceCollection services);
    }
}
