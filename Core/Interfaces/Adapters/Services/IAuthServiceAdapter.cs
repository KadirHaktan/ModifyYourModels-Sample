using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Core.Interfaces.Adapters
{
    public interface IAuthServiceAdapter
    {
        Task<dynamic> GetAccessTokenAdapterAsync();

        dynamic GetAccessTokenAdapter();
    }
}
