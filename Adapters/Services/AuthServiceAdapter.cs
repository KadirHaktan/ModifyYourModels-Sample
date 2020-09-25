using Core.Interfaces.Adapters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Auth;
using Core.Interfaces.Entities.Auth;

namespace Adapters.Services
{
    public class AuthServiceAdapter<T> : IAuthServiceAdapter<T> where T : class, IAuthEntity
    {

        public readonly IAuthService<T> authService;

        public AuthServiceAdapter(IAuthService<T> service)
        {
            this.authService = service;
        }

        public dynamic GetAccessTokenAdapter(T authEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetAccessTokenAdapterAsync(T authEntity)
        {
            return await authService.GetAccessTokenAsync(authEntity);
        }
    }
}
