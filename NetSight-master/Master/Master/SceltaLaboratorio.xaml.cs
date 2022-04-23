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

        //XmlSerializer serializer;
        readonly HttpClient client = new HttpClient();
        private Utente user;

        public SceltaLaboratorio(Utente user)
        {
            InitializeComponent();
            this.user = user;
            Setup();
        }

        private void Setup()
        {
            cmbLab.Items.Clear();
            foreach (string item in riceviDatiListaLab())
            {
                cmbLab.Items.Add(item);
            }
            lblbentornato.Content = "Bentornato " + user.email.Split("@")[0];
        }

        private List<string> riceviDatiListaLab()
        {
            var response = client.GetStringAsync("http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLabsNames.php").Result; //utilizzare link get che ritorna file json. es: netsight.it/getLabs
            return JsonConvert.DeserializeObject<List<string>>(response);
        }

        

        private void btnSceltaLab_Click(object sender, RoutedEventArgs e)
        {
            WindowLaboratorio wlab = new WindowLaboratorio(GetLaboratorio(cmbLab.SelectedItem.ToString()));
            wlab.Show();
            this.Close();
        }

        private Laboratorio GetLaboratorio(string nome)
        {
            var response = client.GetStringAsync("http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLab.php?name="+nome).Result; //utilizzare link get che ritorna file json. es: netsight.it/getLabs
            return JsonConvert.DeserializeObject<Laboratorio>(response);
        }

        private void btnConfPc_Click(object sender, RoutedEventArgs e)
        {
            UdpClient udpClient = new UdpClient();
            byte[] datagram = Encoding.ASCII.GetBytes("apertura");
            //udpClient.Send(datagram, datagram.Length, txtIpPc.Text.ToString(), 24690);
        }
    }
}
