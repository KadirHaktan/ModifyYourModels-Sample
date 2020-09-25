using Core.Interfaces.Auth;
using Entities.Concerete;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Forge;
using Core.Business;
using Core.Utilities.Configurations;

namespace Services.Concerete
{
    public class AuthService : IAuthService
    {
        public readonly ITwoLeggedApi leggedApi;

        private dynamic token { get; set; }
        public AuthService(ITwoLeggedApi api)
        {
            this.leggedApi = api;
        }

        public dynamic GetAccessToken()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetAccessTokenAsync()
        {

            if (token == null || token.ExpiresAt < DateTime.UtcNow)
            { 
                token= await Get2LeggedTokenAsync(new Scope[] { Scope.BucketCreate, Scope.BucketRead, Scope.BucketDelete, Scope.DataRead, Scope.DataWrite, Scope.DataCreate, Scope.CodeAll });
                token.ExpiresAt = DateTime.UtcNow.AddSeconds(token.expires_in);

            }

            return token;

        }


        #region privateMethods

        private async Task<dynamic> Get2LeggedTokenAsync(Scope[] scopes)
        {
            string grantType = "client_credentials";
            string clientId = AppSettings.Get("FORGE_CLIENT_ID");
            string clientSecret = AppSettings.Get("FORGE_CLIENT_SECRET");

            dynamic bearer = await leggedApi.AuthenticateAsync(clientId, clientSecret, grantType, scopes);

            return bearer;

        }







        #endregion


   }
}
