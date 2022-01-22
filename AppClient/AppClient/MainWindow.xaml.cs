using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        //const int DefaultPort = 24690;
        int DefaultPort;
        const string PATH_HOSTNAME = "hostname.txt";
        string Hostname;
        UdpClient server;
        public MainWindow(int port)
        {
            InitializeComponent();
            //Hide();
            DefaultPort = port;
            server = new UdpClient(DefaultPort);
            #region parte hostname
            if (!File.Exists(PATH_HOSTNAME))
            {
                File.Create(PATH_HOSTNAME);
            }
            Hostname = new string(File.ReadAllText(PATH_HOSTNAME).Trim());
            #endregion
            #region inizio thread
            Thread threadRicevi = new Thread(new ThreadStart(Ricevi));
            threadRicevi.Start();
            new Thread(new ThreadStart(() => { while (true) { Invia("alive");Thread.Sleep(10000);  } })).Start();
            #endregion
            MessageBox.Show($"Hostname: {Hostname}:{DefaultPort}");
        }


        void Ricevi()
        {
            while (true)
            {
                IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataReceived = server.Receive(ref riceveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                new Thread(GestioneMessaggio).Start(new object[] { messaggio, riceveEP });
            }
        }
        void Invia(string messaggio)
        {
            UdpClient client = new UdpClient();
            if (Hostname == "") return;
            byte[] data = Encoding.ASCII.GetBytes(messaggio);
            client.Send(data, data.Length, Hostname, DefaultPort);
            client.Close();
        }

        void GestioneMessaggio(object args)
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
                default:
                    break;
            }
        }
    }
}
