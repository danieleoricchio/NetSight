using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetSight
{
    public class Pc
    {
        public bool Stato { get; set; }
        //mettere proprietà pubblica dell'ip (decidere se string o IPAddress)
        public string IP {get;set;}

        public Pc(bool stato)
        {
            this.Stato = stato;
        }

        public void Controllo()
        {
            new Task(() => { /*qua devo controllare che arrivino i pacchetti "alive", se dopo 30 sec (o quanto vuoi)
                              * i pacchetti non arrivano tu metti Stato = false*/ }).Start();
        }

        public void Aggiorna(bool stato)
        {
            this.Stato = stato;
        }

        public bool IniziaCondivisioneSchermo()
        {
            try
            {
                Process.Start("ViewScreen.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            //invia al pc pacchetto udp "condivisione-schermo"
            string mess = "condivisione-schermo";
            try
            {
                new UdpClient().Send(Encoding.ASCII.GetBytes(mess), Encoding.ASCII.GetBytes(mess).Length, IP, 24690);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
    }
}
