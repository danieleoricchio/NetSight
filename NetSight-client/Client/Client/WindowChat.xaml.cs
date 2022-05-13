using Master.Classi;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for WindowChat.xaml
    /// </summary>
    public partial class WindowChat : Window
    {
        string serverIp;
        string clientIp;
        public WindowChat(string ipServer, string thisIp)
        {
            InitializeComponent();
            this.serverIp = ipServer;
            this.clientIp = thisIp;
            lblServer.Content = serverIp;
            receivePackets();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtMsg.Text == "") { MessageBox.Show("Devi scrivere un messaggio"); return; }

            Messaggio msg = new Messaggio(serverIp, clientIp, txtMsg.Text);
            Chat chat = new Chat();
            chat.addMsg(msg);
            myChat.Text += DateTime.Now.ToString("HH:mm") + ", Me >> " + msg.contenuto + DateTime.Now + "\n";
            sendData("messaggio;" + msg.contenuto, serverIp, 24690);
        }

        private void receivePackets()
        {
            UdpClient udpServer = new UdpClient(25000);
            while (true)
            {
                IPEndPoint receiveEP = new IPEndPoint(IPAddress.Any, 0);

                byte[] dataReceived = udpServer.Receive(ref receiveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                string info = messaggio.Split(";")[0];
                string msg_received = messaggio.Split(";")[1];
                new Thread(() =>
                {
                    switch (info)
                    {
                        case "messaggio":
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                myChat.Text += DateTime.Now.ToString("HH:mm") + ", Server >> " + msg_received + "\n";
                            });
                            break;
                        default:
                            break;
                    }
                }).Start();
            }
        }
        private void sendData(string dataIn, string ip, int port)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataIn);
            new UdpClient().Send(data, data.Length, ip, port);
        }
    }
}
