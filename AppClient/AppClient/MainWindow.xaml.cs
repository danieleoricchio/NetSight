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
        const int DefaultPort = 12345;
        const string pathHostname = "hostname.txt";
        string Hostname = new string(File.ReadAllText(pathHostname));
        UdpClient client = new UdpClient(DefaultPort);
        public MainWindow()
        {
            InitializeComponent();
            Hide();
            Thread threadRicevi = new Thread(new ThreadStart(Ricevi));
            threadRicevi.Start();

            new Thread(new ThreadStart(() => { while (true) { Invia("alive"); Thread.Sleep(10000); } })).Start();

            
        }


        void Ricevi()
        {
            while (true)
            {
                IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataReceived = client.Receive(ref riceveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                GestioneMessaggio(messaggio, riceveEP);
            }
        }
        void Invia(string messaggio)
        {
            if (Hostname == "") return;
            byte[] data = Encoding.ASCII.GetBytes(messaggio);
            client.Send(data, data.Length, "79.17.167.114", DefaultPort);
            MessageBox.Show("Mandato");
        }

        void GestioneMessaggio(string messaggio, IPEndPoint pacchetto)
        {
            char richiesta = messaggio[0];
            switch (richiesta)
            {
                case 'a':
                    if (Hostname == "")
                    {
                        Hostname = pacchetto.Address.ToString();
                        MessageBox.Show(Hostname);
                        File.WriteAllText(pathHostname, Hostname);
                    }
                    return;
                default:
                    break;
            }
        }
    }
}
