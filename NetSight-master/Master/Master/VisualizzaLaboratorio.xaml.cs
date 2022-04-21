using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Master
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

        List<Laboratorio> labs;
        Laboratorio lab;
        //XmlSerializer serializer;
        readonly HttpClient client = new HttpClient();
        private bool flagPcConnesso;

        public VisualizzaLaboratorio()
        {
            InitializeComponent();
            labs = new List<Laboratorio>();
            btnAggiungiPc.IsEnabled = false;
            txtLabN.Visibility = Visibility.Hidden;
            lblTitle1.Visibility = Visibility.Hidden;
            lblTitle2.Visibility = Visibility.Hidden;
            txtNumPc.Visibility = Visibility.Hidden;
            btnConfLab.Visibility = Visibility.Hidden;
            labs = riceviDatiListaLab();
            impostaCombobox();
        }

        private void impostaCombobox()
        {
            cmbLab.Items.Clear();
            foreach (Laboratorio item in labs)
            {
                cmbLab.Items.Add(item.nome);
            }
        }

        private List<Laboratorio> riceviDatiListaLab()
        {
            var response = client.GetStringAsync("http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLabs.php").Result; //utilizzare link get che ritorna file json. es: netsight.it/getLabs
            return JsonConvert.DeserializeObject<List<Laboratorio>>(response);
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
                            //labs.GetPc(ip).AggiornaStato(); //fai pc.Aggiorna(true) per dire che il pc è vivo
                            //labs.Find(lab => lab.)
                            break;
                        case "connected":
                            Pc pc = new Pc(true);
                            pc.ip = txtIpPc.Text.ToString();
                            pc.nome = txtNomePc.Text.ToString();
                            //labs.Add(pc);
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
            udpClient.Send(datagram, datagram.Length, txtIpPc.Text.ToString(), 24690);
        }
    }
}
