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
using XMLSerializerTcp;

namespace NetSight
{
    /// <summary>
    /// Logica di interazione per VisualizzaLaboratorio.xaml
    /// </summary>
    public partial class VisualizzaLaboratorio : Window
    {
        static Laboratorio lab,labLetti;
        static Laboratori laboratori,laboratoriLetti;
        XmlSerializerTcp serializerTcp;
        TcpClient client;
        public VisualizzaLaboratorio(string response)
        {
            InitializeComponent();
            client = new TcpClient("172.16.102.67", 666);
            serializerTcp = new XmlSerializerTcp(typeof(Laboratorio));
            lab = new Laboratorio(); 
            labLetti = new Laboratorio();
            btnAggiungiPc.IsEnabled = false;
            txtLabN.Visibility = Visibility.Hidden;
            lblTitle1.Visibility = Visibility.Hidden;
            lblTitle2.Visibility = Visibility.Hidden;
            txtNumPc.Visibility = Visibility.Hidden;
            btnConfLab.Visibility = Visibility.Hidden;
            if (response == "U")
            {
                btnAggiungiPc.Visibility = Visibility.Hidden;
                BtnAggiungiLab.Visibility = Visibility.Hidden;
            }
            riceviDatiListaPc();
            riceviDatiListaLab();
        }

        private void riceviDatiListaLab()
        {
            object obj = serializerTcp.Deserialize(client.GetStream());
            Laboratori laboratori = (Laboratori)obj;
            laboratori = laboratoriLetti;
            for (int i = 0; i < laboratori.listaLab.Count; i++)
            {
                cmbLab.Items.Add(laboratoriLetti.listaLab[i]);
            }
        }

        private void riceviPacchetti()
        {
            UdpClient udpServer = new UdpClient(25000);
            while (true)
            {
                IPEndPoint riceiveEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataReceived = udpServer.Receive(ref riceiveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                new Task(() =>
                {
                    switch (messaggio)
                    {
                        case "alive":
                            riceiveEP.Address.ToString();

                            //sistemo con l'invoke che il pc nella window è acceso
                            //in poche parole metti che il rettangolo del pc è verde
                            //se è online altrimenti rosso
                            //fai pc.Aggiorna(true) per dire che il pc è vivo
                            return;
                        default:
                            break;
                    }
                }).Start();
            }
        }

        private void btnSceltaLab_Click(object sender, RoutedEventArgs e)
        {
            btnAggiungiPc.IsEnabled = true;
        }

        private void btnAggiungiPc_Click(object sender, RoutedEventArgs e)
        {
            btnConfPc.IsEnabled = true;
            txtIpPc.IsEnabled = true;
            lblPc.IsEnabled = true;
        }

        private void BtnAggiungiLab_Click(object sender, RoutedEventArgs e)
        {
            txtLabN.Visibility = Visibility.Visible;
            lblTitle1.Visibility = Visibility.Visible;
            lblTitle2.Visibility = Visibility.Visible;
            txtNumPc.Visibility = Visibility.Visible;
            btnConfLab.Visibility = Visibility.Visible;
        }

        private void btnConfLab_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfPc_Click(object sender, RoutedEventArgs e)
        {
            UdpClient udpClient = new UdpClient();
            byte[] datagram = Encoding.ASCII.GetBytes("apertura");
            udpClient.Send(datagram, datagram.Length,txtIpPc.Text.ToString(), 24690);

            IPEndPoint riceiveEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] dataReceived = udpClient.Receive(ref riceiveEP);
            string messaggio = Encoding.ASCII.GetString(dataReceived);
            if (messaggio == "connected")
            {
                Pc pc = new Pc(true);
                pc.IP = txtIpPc.Text.ToString();
                lab.addPc(pc);
            }
            /*
             * qua devi mandare pacchetto UDP "apertura" al pc che 
             * bisogna aggiungere (la port d'arrivo è 24690)
             */
        }

        private void riceviDatiListaPc()
        {
            object obj = serializerTcp.Deserialize(client.GetStream());
            Laboratorio labLetti = (Laboratorio)obj;
            lab = labLetti;
            //for (int i = 0; i < lab.listaPc.Count; i++)
            //{
            //    cmbLab.Items.Add(labLetti.listaPc[i]);
            //}
        }
    }
}
