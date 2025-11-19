using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DOMS_TSR
{
    public class myChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void MyChatSend(string name, string to, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, to, message);
        }
    }
}