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
using System.Windows.Navigation;
using System.Windows.Shapes;
using XMLSerializerTcp;
namespace NetSight
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Utente u;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnAccesso_Click(object sender, RoutedEventArgs e)
        {
            u = new Utente(txtNomeUtente.Text, psw.Password.ToString());
            string response = u.httpRequest();
            if (response != "A" || response != "U")
            {
                VisualizzaLaboratorio visualizzaLaboratorio = new VisualizzaLaboratorio(response);
                visualizzaLaboratorio.Show();
                this.Close();
            }
            else
                MessageBox.Show("Nome utente/password non trovato/i.", "Errore");
        }
    }
}
