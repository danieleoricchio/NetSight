using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NetSight
{
    /// <summary>
    /// Logica di interazione per VisualizzaLaboratorio.xaml
    /// </summary>
    public partial class VisualizzaLaboratorio : Window
    {
        static Laboratorio lab,labLetti;
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
            riceviDati();
        }

        private void btnSceltaLab_Click(object sender, RoutedEventArgs e)
        {
            btnAggiungiPc.IsEnabled = true;
        }

        private void btnAggiungiPc_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void riceviDati()
        {
            object obj = serializerTcp.Deserialize(client.GetStream());
            Laboratorio labLetti = (Laboratorio)obj;
            lab = labLetti;
            for (int i = 0; i < lab.listaPc.Count; i++)
            {
                cmbLab.Items.Add(labLetti.listaPc[i]);
            }
        }
    }
}
