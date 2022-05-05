using Client.Classi;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            txt_mail.Text = Properties.Settings.Default.email;
            txt_password.Password = Properties.Settings.Default.password;
            checkBox_remember.IsChecked = true;
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if(checkBox_remember.IsChecked == true)
            {
                Properties.Settings.Default.email = txt_mail.Text;
                Properties.Settings.Default.password = txt_password.Password;
                Properties.Settings.Default.Save();
            }

            string mail = txt_mail.Text;
            string password = txt_password.Password.ToString();
            if (mail == "" || password == "") { MessageBox.Show("Inserire tutti i dati"); return; }

            var values = new Dictionary<string, string> { { "email", mail }, { "password", password } };
            JsonMessage? message = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_confirmLogin, values);

            if (message == null)
            {
                MessageBox.Show("Login non effettuato.", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_mail.Text = "";
                txt_password.Password = "";
                return;
            }
            if (!message.status)
            {
                MessageBox.Show("Login non effettuato. "+message.message, "Errore", MessageBoxButton.OK, MessageBoxImage.Hand); 
                txt_mail.Text = ""; 
                txt_password.Password = ""; 
                return;
            }
            MainApp app = new MainApp(24690);
            GestioneFinestra gestioneFinestra = new GestioneFinestra(app);
            gestioneFinestra.Show();
            this.Close();
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            Registrazione r = new Registrazione();
            r.Show();
            this.Close();
        }
    }
}