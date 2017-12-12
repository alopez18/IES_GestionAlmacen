using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ALC.IES.GestionAlmacen.Hubs {
    public class TerminalesHub : Hub {



        public void ActivarTerminal(int idTerminal, String usuario) {
            var context = GlobalHost.ConnectionManager.GetHubContext<TerminalesHub>();
            context.Clients.All.activarTerminal(idTerminal, usuario);
            //Clients.All.activarTerminal(idTerminal, usuario);
        }

        public void DesactivarTerminal(int idTerminal) {
            var context = GlobalHost.ConnectionManager.GetHubContext<TerminalesHub>();
            context.Clients.All.desactivarTerminal(idTerminal);
            //Clients.All.activarTerminal(idTerminal, usuario);
        }

        public void SetPCA2Terminal(int idPCA, int idTerminal, String model) {
            var context = GlobalHost.ConnectionManager.GetHubContext<TerminalesHub>();
            context.Clients.All.setPCA2Terminal(idPCA, idTerminal, model);

            //Clients.All.activarTerminal(idTerminal, usuario);
        }

        internal void MovePCA(int idPCA, int terminalOld, int terminalNew, string model) {
            var context = GlobalHost.ConnectionManager.GetHubContext<TerminalesHub>();
            context.Clients.All.movePCA(idPCA, terminalOld,terminalNew, model);

            //Clients.All.activarTerminal(idTerminal, usuario);
        }

        internal void RefreshPCAinTerminales(int idPCA) {
            var context = GlobalHost.ConnectionManager.GetHubContext<TerminalesHub>();
            context.Clients.All.refreshPCAinTerminales(idPCA);

            //Clients.All.activarTerminal(idTerminal, usuario);
        }

        
    }//Class Finish
}//Namespace Finish