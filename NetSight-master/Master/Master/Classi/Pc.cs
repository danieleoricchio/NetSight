using Master.Classi;
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
        private Chat chat;
        public Pc(bool stato, string nome, string ip)
        {
            this.nome = nome;
            this.ip = ip;
            this.stato = stato;
            chat = new Chat();
            timer = new System.Timers.Timer(10000);
            timer.Elapsed += (object? sender, ElapsedEventArgs e) =>
            {
                timer.Stop();
                AggiornaStato(false);
            };
        }
        public Pc()
        {

        }
        public void Controllo()
        {
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += (object? sender, ElapsedEventArgs e) =>
            {
                timer.Stop();
                AggiornaStato(false);
            };
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
    }
}
