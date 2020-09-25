using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Entities.Auth
{
    public interface IAuthEntity:IEntity
    {
        dynamic InternalToken { get; set; }
    }
}
