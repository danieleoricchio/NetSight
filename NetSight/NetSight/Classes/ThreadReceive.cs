using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace NetSight.Classes
{
    class ThreadReceive
    {
        private const int listenPort = 12345;

        UdpClient server;
        IPEndPoint riceveEp;
        string data;
        Pc pc;
        Thread? t;
        GestionePc gestionePc;

        public ThreadReceive()
        {
            server = new UdpClient(listenPort);
            riceveEp = new IPEndPoint(IPAddress.Any, 0);
            data = string.Empty;
            pc= new Pc();
            gestionePc = new GestionePc();
        }

        public void run()
        {
            try
            {
                while (true)
                {
                    byte[] bytes = server.Receive(ref riceveEp);
                    data = Encoding.ASCII.GetString(bytes);
                    AddPc(data);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
                MessageBox.Show("Errore di ricezione", "Errore di ricezione", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void AddPc(string data)
        {
            string[] fields = data.Split(";");
            MessageBox.Show(data);
            pc = new Pc(fields[0], fields[1], Convert.ToBoolean(fields[2]), fields[3]);
            gestionePc.AddPc(pc);
            DatabaseControl.ConnectToDatabase();
            DatabaseControl.AddToDatabase("pc", gestionePc.pcList);
        }

        public void StartThreadRicezione()
        {
            t = new Thread(new ThreadStart(run));
            t.Start();
        }
    }
}
