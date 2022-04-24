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
        public SceltaLaboratorio(Utente user)
        {
            InitializeComponent();
            this.user = user;
            Setup();
        }

        private void Setup()
        {
            cmbEdifici.Items.Clear();
            foreach (Edificio item in PhpLinkManager.GetMethod<List<Edificio>>(PhpLinkManager.URL_getEdifici))
            {
                cmbEdifici.Items.Add(item.nome);
            }
            string nome = user.email.Split("@")[0];
            lblbentornato.Content = "Bentornato " + user.email.Split("@")[0];
            lblLabDisp.Visibility = Visibility.Collapsed;
            cmbLab.Visibility = Visibility.Collapsed;
            btnSceltaLab.Visibility = Visibility.Collapsed;
            btnAggiungiLaboratorio.Visibility = Visibility.Collapsed;
        }

        private void btnSceltaLab_Click(object sender, RoutedEventArgs e)
        {
            if(cmbLab.SelectedItem != null)
            {
                Laboratorio laboratorio = PhpLinkManager.GetMethod<Laboratorio>(PhpLinkManager.URL_getLab + cmbLab.SelectedItem.ToString());
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
                List<Edificio> edificios = PhpLinkManager.GetMethod<List<Edificio>>(PhpLinkManager.URL_getEdifici);
                if (edificios == null)
                {
                    MessageBox.Show("Errore");
                    return;
                }
                edificio = edificios[cmbEdifici.SelectedIndex];
                lblLabDisp.Visibility = Visibility.Visible;
                cmbLab.Visibility = Visibility.Visible;
                btnSceltaLab.Visibility = Visibility.Visible;
                btnAggiungiLaboratorio.Visibility = Visibility.Visible;
                lblEdDisp.Visibility = Visibility.Collapsed;
                cmbEdifici.Visibility = Visibility.Collapsed;
                btnSceltaEdificio.Visibility = Visibility.Collapsed;
                btnAggiungiEdificio.Visibility = Visibility.Collapsed;
                cmbLab.Items.Clear();
                foreach (string item in PhpLinkManager.GetMethod<List<string>>(PhpLinkManager.URL_getLabsNames+edificio.cod))
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
            WindowAdd windowAdd = new WindowAdd("laboratorio", user);
            windowAdd.Show();
            this.Close();
        }
    }
}
