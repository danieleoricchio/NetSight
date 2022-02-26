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
using System.Windows.Shapes;
using System.Xml.Serialization;
using XMLSerializerTcp;

namespace NetSight
{
    /// <summary>
    /// Logica di interazione per VisualizzaLaboratorio.xaml
    /// </summary>
    public partial class VisualizzaLaboratorio : Window
    {
        /**
         * appena crei tutti i rettangoli dei pc con il codice
         * devi avviare un thread che controlla tutti gli stati dei pc
         * e aggiorna i rettangoli
         */

        static Laboratorio lab,labLetti;
        static Laboratori laboratori,laboratoriLetti;
        XmlSerializer serializer;
        private bool flagPcConnesso;

        public VisualizzaLaboratorio(string response)
        {
            InitializeComponent();
            serializer = new XmlSerializer(typeof(Laboratorio));
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
            StreamReader streamReader = new StreamReader("listaLab.xml");
            object obj = serializer.Deserialize(streamReader);
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
                            string ip = riceiveEP.Address.ToString();
                            lab.GetPc(ip).AggiornaStato();                            //fai pc.Aggiorna(true) per dire che il pc è vivo
                            break;
                        case "connected":
                            Pc pc = new Pc(true);
                            pc.IP = txtIpPc.Text.ToString();
                            pc.Nome= txtNomePc.Text.ToString();
                            lab.addPc(pc);
                            break;
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
        }

        private void riceviDatiListaPc()
        {
            StreamReader streamReader = new StreamReader("listaPc.xml");
            object obj = serializer.Deserialize(streamReader);
            Laboratorio labLetti = (Laboratorio)obj;
            lab = labLetti;
            for (int i = 0; i < lab.listaPc.Count; i++)
            {
                cmbLab.Items.Add(labLetti.listaPc[i]);
            }
        }
    }
}
