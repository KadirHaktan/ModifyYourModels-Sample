using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class DesignAutomationHub:Hub
    {
        public string ConnectionId()
        {
            return Context.ConnectionId;
        }

      
    }
}
