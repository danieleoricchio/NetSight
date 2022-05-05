using Client.Classi;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Client
{
    /// <summary>
    /// Logica di interazione per Registrazione.xaml
    /// </summary>
    public partial class Registrazione : Window
    {
        public Registrazione()
        {
            InitializeComponent();
        }

        private void btn_registra_Click(object sender, RoutedEventArgs e)
        {
            string nome = txt_nome.Text;
            string cognome = txt_cognome.Text;
            string mail = txt_mail.Text;
            string data = txt_data.Text;
            string password = txt_password.Text;
            string conferma = txt_conferma.Text;

            if (nome == "" || cognome == "" || mail == "" || data == "" || password == "" || conferma == "") { MessageBox.Show("Inserire tutti i dati"); return; }

            if (password != conferma) { MessageBox.Show("Inserire due password uguali"); return; }

            var values = new Dictionary<string, string> { { "nome", nome }, { "cognome", cognome }, { "mail", mail }, { "data", data }, { "password", password } };

            JsonMessage? registerResponse = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_confirmLogin, values);
            if (registerResponse == null)
            {
                MessageBox.Show("Registrazione non effettuata", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!registerResponse.status)
            {
                MessageBox.Show("Registrazione non effettuata. " + registerResponse.message, "Errore", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            MainApp app = new MainApp(24690);
            this.Close();
        }
        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Login w = new Login();
            w.Show();
            this.Close();
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_nome.Text = "";
            txt_cognome.Text = "";
            txt_mail.Text = "";
            txt_data.Text = "";
            txt_password.Text = "";
            txt_conferma.Text = "";
        }
    }
}