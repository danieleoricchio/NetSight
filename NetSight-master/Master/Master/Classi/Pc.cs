using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Master
{
    public class Pc
    {
        public int cod { get; set; }
        public bool stato { get; set; }
        public string ip {get;set;}
        public string nome {get;set; }
        private readonly object Locked = new object();
        private System.Timers.Timer timer;
        public Pc(bool stato, string nome, string ip)
        {
            this.stato = stato;
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += (object? sender, ElapsedEventArgs e) =>
            {
                timer.Stop();
                AggiornaStato(false);
            };
        }


        public void Controllo()
        {
            new Thread(() => {
                while (true)
                {
                    if (stato && !timer.Enabled)
                    {
                        timer.Start();
                    }
                    Thread.Sleep(100);
                }
            }).Start();
        }

        public void AggiornaStato(bool stato)
        {
            lock (Locked)
            {
                this.stato = stato;
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
                new UdpClient().Send(Encoding.ASCII.GetBytes(mess), Encoding.ASCII.GetBytes(mess).Length, ip, 24690);
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
