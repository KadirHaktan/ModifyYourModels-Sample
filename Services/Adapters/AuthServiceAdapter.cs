using Core.Interfaces.Adapters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Auth;
using Core.Interfaces.Entities.Auth;

namespace Services.Adapters
{
    public class AuthServiceAdapter: IAuthServiceAdapter
    {

        public readonly IAuthService authService;

        public AuthServiceAdapter(IAuthService service)
        {
            this.authService = service;
        }

        public dynamic GetAccessTokenAdapter()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetAccessTokenAdapterAsync()
        {
            return await authService.GetAccessTokenAsync();
        }
    }
}
