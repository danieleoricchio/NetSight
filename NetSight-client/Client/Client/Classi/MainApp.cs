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
        private string hostname;
        public string labIp;
        public string thisIp;
        private UdpClient server;
        public bool connesso = false;
        public MainApp(int port)
        {
            try
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
                hostname = File.ReadAllText(PATH_HOSTNAME).Trim();
                labIp = hostname;
                #endregion
                #region inizio thread
                new Thread(new ThreadStart(Ricevi)).Start();
                new Thread(new ThreadStart(() => { while (true) { if (connesso) Invia("alive;" + thisIp, 25000); Thread.Sleep(10000); } })).Start();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private volatile bool flagChiusura = false;
        public bool ChiediDisconnessione()
        {
            bool timerScaduto = false;
            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += (object? sender, System.Timers.ElapsedEventArgs e) => { timerScaduto = true; };
            Invia("chiedi-chiusura", 25000);
            timer.Start();
            while (!flagChiusura && !timerScaduto)
            {
                new Object();
            }
            timer.Stop();
            if (timerScaduto) return false;
            return true;
        }

        private void Ricevi()
        {
            while (true)
            {
                IPEndPoint receiveEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataReceived = server.Receive(ref receiveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                new Thread(() =>
                {
                    switch (messaggio)
                    {
                        case "apertura":
                            if (hostname.Trim() == "" || labIp == "")
                            {
                                hostname = receiveEP.Address.ToString();
                                labIp = hostname;
                                connesso = true;
                                File.WriteAllText(PATH_HOSTNAME, hostname);
                                Invia("apertura-confermata", 25000);
                            }
                            return;
                        case "chiusura":
                            connesso = false;
                            labIp = "";
                            flagChiusura = true;
                            return;
                        case "condivisione-schermo":
                            if (Process.Start("ScreenSharing.exe", $"hostname={hostname} port=5900 width=1280 height=720") != null)
                            {
                                MessageBox.Show("Partito");
                            }
                            return;
                        case "riapertura":
                            if(labIp == receiveEP.Address.ToString())
                            {
                                connesso = true;
                                Invia("riapertura-confermata;" + thisIp, 25000);
                            }
                            return;
                        default:
                            break;
                    }
                }).Start();
            }
        }
        private void Invia(string messaggio, int port)
        {
            if (labIp == "") return;
            UdpClient client = new UdpClient();
            byte[] data = Encoding.ASCII.GetBytes(messaggio);
            client.Send(data, data.Length, hostname, port);
            client.Close();
        }
    }
}
