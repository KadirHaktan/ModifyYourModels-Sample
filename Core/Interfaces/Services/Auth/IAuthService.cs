using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        dynamic GetAccessToken();

        Task<dynamic> GetAccessTokenAsync();
    }
}
