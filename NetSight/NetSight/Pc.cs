using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace NetSight
{
    public class Pc
    {
        public bool stato { get; set; }
        public string ip {get;set;}
        public string nome {get;set; }
        private readonly object Locked = new object();
        private Timer timer;
        private volatile bool flagAlive;
        public Pc(bool stato)
        {
            this.Stato = stato;
            timer = new Timer(30000);
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                Stato = false;
            };
            if(stato)
                timer.Start();
            Controllo();
        }


        public void Controllo()
        {
            new Task(() => {
                while (true)
                {
                    if (flagAlive)
                    {
                        flagAlive = false;
                        timer.Start();
                    }
                }
            }).Start();
        }

        public void AggiornaStato()
        {
            lock (Locked)
            {
                flagAlive = true;
                this.Stato = true;
            }
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
