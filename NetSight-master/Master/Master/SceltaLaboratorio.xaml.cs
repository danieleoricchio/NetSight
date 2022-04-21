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
    public partial class SceltaLaboratorio : Window
    {
        /**
         * appena crei tutti i rettangoli dei pc con il codice
         * devi avviare un thread che controlla tutti gli stati dei pc
         * e aggiorna i rettangoli
         */

        Laboratori labs;
        Laboratorio lab;
        //XmlSerializer serializer;
        readonly HttpClient client = new HttpClient();
        private bool flagPcConnesso;
        private Utente user;

        public SceltaLaboratorio(Utente user)
        {
            InitializeComponent();
            this.user = user;
            Setup();
        }

        private void Setup()
        {
            labs = new Laboratori();
            labs.listaLab = riceviDatiListaLab();
            cmbLab.Items.Clear();
            foreach (Laboratorio item in labs.listaLab)
            {
                cmbLab.Items.Add(item.nome);
            }
            lblbentornato.Content = "Bentornato " + user.email.Split("@")[0];
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
                            //pc.ip = txtIpPc.Text.ToString();
                            //pc.nome = txtNomePc.Text.ToString();
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
            WindowLaboratorio wlab = new WindowLaboratorio(labs.listaLab[cmbLab.SelectedIndex]);
            wlab.Show();
            this.Close();
        }

        private void btnConfPc_Click(object sender, RoutedEventArgs e)
        {
            UdpClient udpClient = new UdpClient();
            byte[] datagram = Encoding.ASCII.GetBytes("apertura");
            //udpClient.Send(datagram, datagram.Length, txtIpPc.Text.ToString(), 24690);
        }
    }
}
