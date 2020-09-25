using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Entities.Auth;

namespace Core.Interfaces.Auth
{
    public interface IAuthService
    {
        dynamic GetAccessToken();

        Task<dynamic> GetAccessTokenAsync();
    }
}
