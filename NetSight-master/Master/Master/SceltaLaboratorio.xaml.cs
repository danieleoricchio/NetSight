using Master.Classi;
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
        private readonly HttpClient client = new HttpClient();
        private Utente user;
        private const string url_getLabsNames = "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLabsNames.php?codedificio=";
        private const string url_getLab = "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLab.php?name=";
        private const string url_getEdifici = "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getEdifici.php";
        private Edificio edificio;
        public SceltaLaboratorio(Utente user)
        {
            InitializeComponent();
            this.user = user;
            Setup();
        }

        private void Setup()
        {
            cmbEdifici.Items.Clear();
            foreach (Edificio item in riceviDatiListaEdifici())
            {
                cmbEdifici.Items.Add(item.nome);
            }
            lblbentornato.Content = "Bentornato " + user.email.Split("@")[0];
            lblLabDisp.Visibility = Visibility.Collapsed;
            cmbLab.Visibility = Visibility.Collapsed;
            btnSceltaLab.Visibility = Visibility.Collapsed;
        }

        private List<string> riceviDatiListaLab()
        {
            return JsonConvert.DeserializeObject<List<string>>(client.GetStringAsync(url_getLabsNames + edificio.cod).Result);
        }

        private List<Edificio> riceviDatiListaEdifici()
        {
            return JsonConvert.DeserializeObject<List<Edificio>>(client.GetStringAsync(url_getEdifici).Result);
        }

        private void btnSceltaLab_Click(object sender, RoutedEventArgs e)
        {
            if(cmbLab.SelectedItem != null)
            {
                WindowLaboratorio wlab = new WindowLaboratorio(GetLaboratorio(cmbLab.SelectedItem.ToString()), user);
                wlab.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Selezionare il laboratorio", "Errore di selezione");
            }
        }

        private Laboratorio GetLaboratorio(string nome)
        {
            return JsonConvert.DeserializeObject<Laboratorio>(client.GetStringAsync(url_getLab + nome).Result);
        }

        private void btnSceltaEdificio_click(object sender, RoutedEventArgs e)
        {
            if(cmbEdifici.SelectedItem != null)
            {
                edificio = riceviDatiListaEdifici()[cmbEdifici.SelectedIndex];
                lblLabDisp.Visibility = Visibility.Visible;
                cmbLab.Visibility = Visibility.Visible;
                btnSceltaLab.Visibility = Visibility.Visible;
                lblEdDisp.Visibility = Visibility.Collapsed;
                cmbEdifici.Visibility = Visibility.Collapsed;
                btnSceltaEdificio.Visibility = Visibility.Collapsed;
                cmbLab.Items.Clear();
                foreach (string item in riceviDatiListaLab())
                {
                    cmbLab.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Selezionare l'edificio", "Errore di selezione");
            }
        }

        private void btnIndietroPage1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
