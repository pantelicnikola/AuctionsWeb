using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionsWeb.MyHub
{
    public class MyHub : Hub
    {
        public void Announce(string announcement)
        {
            Clients.All.Announce(announcement);
        }
    }
}