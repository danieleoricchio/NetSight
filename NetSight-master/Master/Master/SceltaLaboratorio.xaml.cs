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
        //private readonly HttpClient client = new HttpClient();
        private Utente user;
        private Edificio edificio;
        private List<Dictionary<string, string>> edifici;
        public SceltaLaboratorio(Utente user)
        {
            InitializeComponent();
            this.user = user;
            Setup();
        }

        private void Setup()
        {
            cmbEdifici.Items.Clear();
            JsonMessage message = PhpLinkManager.GetMethod<JsonMessage>(PhpLinkManager.URL_getEdificiNames,null);
            edifici = message.GetResultArray<Dictionary<string, string>>();
            foreach (var item in edifici)
            {
                cmbEdifici.Items.Add(item["nome"]);
            }
            lblbentornato.Content = "Bentornato " + user.nome;
            lblLabDisp.Visibility = Visibility.Collapsed;
            cmbLab.Visibility = Visibility.Collapsed;
            btnSceltaLab.Visibility = Visibility.Collapsed;
            btnAggiungiLaboratorio.Visibility = Visibility.Collapsed;
        }

        private void btnSceltaLab_Click(object sender, RoutedEventArgs e)
        {
            if(cmbLab.SelectedItem != null)
            {
                Laboratorio laboratorio = PhpLinkManager.GetMethod<Laboratorio>(PhpLinkManager.URL_getLab, new Dictionary<string, string>() { { "name", cmbLab.SelectedItem.ToString() } });
                if (laboratorio == null)
                {
                    MessageBox.Show("Errore");
                    return;
                }
                WindowLaboratorio wlab = new WindowLaboratorio(laboratorio, user);
                wlab.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Selezionare il laboratorio", "Errore di selezione");
            }
        }

        private void btnSceltaEdificio_click(object sender, RoutedEventArgs e)
        {
            if(cmbEdifici.SelectedItem != null)
            {
                JsonMessage message = PhpLinkManager.GetMethod<JsonMessage>(PhpLinkManager.URL_getEdificio, new Dictionary<string, string>() { { "codedificio", edifici[cmbEdifici.SelectedIndex]["cod"] } });
                edificio = message.GetResultObject<Edificio>();
                if (edificio == null)
                {
                    MessageBox.Show("Errore");
                    return;
                }
                lblLabDisp.Visibility = Visibility.Visible;
                cmbLab.Visibility = Visibility.Visible;
                btnSceltaLab.Visibility = Visibility.Visible;
                btnAggiungiLaboratorio.Visibility = Visibility.Visible;
                lblEdDisp.Visibility = Visibility.Collapsed;
                cmbEdifici.Visibility = Visibility.Collapsed;
                btnSceltaEdificio.Visibility = Visibility.Collapsed;
                btnAggiungiEdificio.Visibility = Visibility.Collapsed;
                cmbLab.Items.Clear();
                foreach (string item in PhpLinkManager.GetMethod<JsonMessage>(PhpLinkManager.URL_getLabsNames, new Dictionary<string, string>() { { "codedificio", edificio.cod.ToString() }, { "codadmin", user.cod.ToString()} }).GetResultArray<string>())
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

        private void btnAggiungiEdificio_Click(object sender, RoutedEventArgs e)
        {
            
            WindowAdd windowAdd = new WindowAdd("edificio", user);
            windowAdd.Show();
            this.Close();
        }

        private void btnAggiungiLaboratorio_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd windowAdd = new WindowAdd(edificio, user);
            windowAdd.Show();
            this.Close();
        }
    }
}
