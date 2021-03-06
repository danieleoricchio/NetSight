using System;
using System.Diagnostics;
using System.IO;
using System.Net;
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
                string ipaddress = "";
                try
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        ipaddress = endPoint.Address.ToString();
                    }
                    thisIp = ipaddress;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                }

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
                labIp = "";
                #endregion
                #region inizio thread
                new Thread(new ThreadStart(Ricevi)).Start();
                new Thread(new ThreadStart(() => { while (true) { if (connesso) Invia("alive", 25000); Thread.Sleep(2500); } })).Start();
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
                Console.Write(string.Empty);
                Thread.Sleep(100);
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
                //MessageBox.Show($"'{messaggio}' from {receiveEP.Address.ToString()}", "Messaggio", MessageBoxButton.OK, MessageBoxImage.Information);
                //MessageBox.Show($"hostname.Trim() = {hostname.Trim()}, labIp = {labIp}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                new Thread(() =>
                {
                    try
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
                                    Invia("apertura-confermata", 24690);
                                    //MessageBox.Show($"apertura-confermata inviata", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                return;
                            case "chiusura":
                                connesso = false;
                                labIp = "";
                                hostname = "";
                                flagChiusura = true;
                                return;
                            case "condivisione-schermo":
                                try
                                {
                                    //ScreenSharing screenSharing = new ScreenSharing();
                                    Process.Start("ScreenSharing.exe", $"hostname={labIp} port=5900 width=1280 height=720");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Errore di condivisione", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                return;
                            case "riapertura":
                                if (hostname == receiveEP.Address.ToString())
                                {
                                    connesso = true;
                                    labIp = hostname;
                                    Invia("riapertura-confermata", 25000);
                                }
                                return;
                            case "apertura-chat":
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    WindowChat windowChat = new WindowChat(labIp, thisIp);
                                    windowChat.Show();
                                }));
                                return;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
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
