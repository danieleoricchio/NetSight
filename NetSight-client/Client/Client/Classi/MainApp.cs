using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace Client
{
    public class MainApp
    {
        private const int DefaultPort = 24690;
        private const string PATH_HOSTNAME = "hostname";
        private string Hostname /*= "79.41.117.203"*/;
        private string thisIp/* = Dns.GetHostEntry(Dns.GetHostName()).AddressList[3].ToString()*/;
        private UdpClient server;
        public MainApp(int port)
        {
            #region get local ip
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipaddress = NetworkInterface.GetAllNetworkInterfaces()
                .First(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || x.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .GetIPProperties().UnicastAddresses.First(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork).Address;
            thisIp = ipaddress.ToString();
            #endregion
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
            new Thread(new ThreadStart(() => { while (true) { Invia("alive;"+thisIp, 25000); Thread.Sleep(10000); } })).Start();
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
        private void Invia(string messaggio, int port)
        {
            if (Hostname == "") return;
            UdpClient client = new UdpClient();
            byte[] data = Encoding.ASCII.GetBytes(messaggio);
            client.Send(data, data.Length, Hostname, port);
            client.Close();
        }
        private void GestioneMessaggio(object args)
        {
            object[] array = (object[])args;
            string messaggio = (string)array.GetValue(0);
            IPEndPoint pacchetto = (IPEndPoint)array.GetValue(1);
            //char richiesta = messaggio[0];
            //MessageBox.Show(messaggio);
            switch (messaggio) 
            {
                case "apertura":
                    if (Hostname.Trim() == "")
                    {
                        Hostname = pacchetto.Address.ToString();
                        File.WriteAllText(PATH_HOSTNAME, Hostname);
                        Invia("apertura-confermata",24690);
                    }
                    return;
                case "condivisione-schermo":

                    if (Process.Start("ScreenSharing.exe", $"hostname={Hostname} port=5900 width=1280 height=720") != null){
                        MessageBox.Show("Partito");
                    }
                    return;
                default:
                    break;
            }
        }
    }
}
