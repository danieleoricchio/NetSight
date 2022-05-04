using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

namespace Master
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Utente utente;
        public MainWindow()
        {
            InitializeComponent();
            txtEmail.Text = Properties.Settings.Default.email;
            psw.Password = Properties.Settings.Default.password;
            checkBox_remember.IsChecked = true;
        }
        private void btnAccesso_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox_remember.IsChecked == true)
            {
                Properties.Settings.Default.email = txtEmail.Text;
                Properties.Settings.Default.password = psw.Password;
                Properties.Settings.Default.Save();
            }

            utente = Utente.GetUserObject(txtEmail.Text, psw.Password.ToString());
            if (utente != null)            {
                SceltaLaboratorio visualizzaLaboratorio = new SceltaLaboratorio(utente);
                visualizzaLaboratorio.Show();
                this.Close();
            }
            else
                MessageBox.Show("Email e/o password sbagliati. Altrimenti possibile timeout nella richiesta", "Errore");
        }
    }
}
