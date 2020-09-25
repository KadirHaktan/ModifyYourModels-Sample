using Core.Interfaces.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Entities.Abstract
{
    public abstract class BaseAuthEntity : IAuthEntity
    {
        public virtual dynamic? InternalToken { get; set; }
    }
}
