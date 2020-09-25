using System;
using System.Collections.Generic;
using System.Text;
using Entities.Abstract;

namespace Entities.Concerete
{
   public class AuthEntity:BaseAuthEntity
    {
        public override dynamic? InternalToken { get; set; }
    }
}
