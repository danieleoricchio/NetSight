using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    public class MainApp
    {
        private const int DefaultPort = 24690;
        private const string PATH_HOSTNAME = "hostname.txt";
        private string Hostname;
        private UdpClient server;
        public MainApp(int port)
        {
            #region setup udp server
            try
            {
                server = new UdpClient(DefaultPort);
            }
            catch (Exception)
            {
                ;
            }
            #endregion
            #region parte hostname
            if (!File.Exists(PATH_HOSTNAME))
            {
                File.Create(PATH_HOSTNAME);
            }
            Hostname = File.ReadAllText(PATH_HOSTNAME).Trim();
            #endregion
            #region inizio thread
            new Thread(new ThreadStart(Ricevi)).Start();
            new Thread(new ThreadStart(() => { while (true) { Invia("alive"); Thread.Sleep(10000); } })).Start();
            #endregion
        }
        private void Ricevi()
        {
            while (true)
            {
                IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataReceived = server.Receive(ref riceveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                new Thread(GestioneMessaggio).Start(new object[] { messaggio, riceveEP });
            }
        }
        private void Invia(string messaggio)
        {
            UdpClient client = new UdpClient();
            if (Hostname == "") return;
            byte[] data = Encoding.ASCII.GetBytes(messaggio);
            client.Send(data, data.Length, Hostname, DefaultPort);
            client.Close();
        }
        private void GestioneMessaggio(object args)
        {
            object[] array = (object[])args;
            string messaggio = (string)array.GetValue(0);
            IPEndPoint pacchetto = (IPEndPoint)array.GetValue(1);
            //char richiesta = messaggio[0];
            MessageBox.Show(messaggio);
            switch (messaggio)
            {
                case "apertura":
                    if (Hostname.Trim() == "")
                    {
                        Hostname = pacchetto.Address.ToString();
                        File.WriteAllText(PATH_HOSTNAME, Hostname);
                    }
                    return;
                case "condivisione-schermo":
                    Process.Start("ScreenSharing.exe", $"hostname=172.16.102.125 port=5900 width=1280 height=720");
                    return;
                default:
                    break;
            }
        }
    }
}
