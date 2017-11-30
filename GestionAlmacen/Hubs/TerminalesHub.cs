using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ALC.IES.GestionAlmacen.Hubs {
    public class TerminalesHub : Hub {




        public void Hello() {
            Clients.All.hello();
        }





    }//Class Finish
}//Namespace Finish