using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Adapters;
using Entities.Concerete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers
{
    
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceAdapter _authServiceAdapter;

        public AuthController(IAuthServiceAdapter authServiceAdapter)
        {
            this._authServiceAdapter = authServiceAdapter;

            
        }

        
        [HttpGet("api/forge/auth/token")]
        public async Task<dynamic> GetToken()
        {
            return await _authServiceAdapter.GetAccessTokenAdapterAsync();
        }
    }
}