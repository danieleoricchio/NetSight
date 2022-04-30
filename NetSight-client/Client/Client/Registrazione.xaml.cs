using Client.Classi;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

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
            this.WindowState = WindowState.Maximized;
        }

        private async void btn_registra_Click(object sender, RoutedEventArgs e)
        {
            string nome = txt_nome.Text;
            string cognome = txt_cognome.Text;
            string mail = txt_mail.Text;
            string data = date.Text;
            string password = txt_password.Text;
            string conferma = txt_conferma.Text;

            if (nome == "" || cognome == "" || mail == "" || data == "" || password == "" || conferma == "") { MessageBox.Show("Inserire tutti i dati"); return; }

            if (password != conferma) { MessageBox.Show("Inserire due password uguali"); return; }

            var values = new Dictionary<string, string> { { "nome", nome }, { "cognome", cognome }, { "mail", mail }, { "data", data }, { "password", password } };

            JsonMessage? registerResponse = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_confirmLogin, values);
            if (registerResponse == null || !registerResponse.result) { MessageBox.Show("Registrazione non effettuata"); return; }

            MessageBox.Show("Registrazione effettuata");
            MainApp app = new MainApp(24690);
            this.Close();
        }

        private void btn_registra_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_registra.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
            btn_registra.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 0, 0, 0));
        }

        private void btn_registra_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_registra.Background = null;
            btn_registra.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
        }

        private void lbl_indietro_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_indietro.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 0, 0, 0));
        }

        private void lbl_indietro_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_indietro.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
        }

        private void lbl_indietro_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Login w = new Login();
            w.Show();
            this.Close();
        }
    }
}