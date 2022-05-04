using Client.Classi;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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
            this.WindowState = WindowState.Maximized;
        }

        private void lbl_link_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_link.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 0, 0, 0));
        }

        private void lbl_link_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_link.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
        }

        private void lbl_link_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Registrazione r = new Registrazione();
            r.Show();
            this.Close();
        }

        private void btn_login_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_login.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
            btn_login.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 0, 0, 0));
        }

        private void btn_login_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_login.Background = null;
            btn_login.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {
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

            MessageBox.Show("Login effettuato","",MessageBoxButton.OK,MessageBoxImage.Information);
            MainApp app = new MainApp(24690);
            GestioneFinestra gestioneFinestra = new GestioneFinestra(app);
            gestioneFinestra.Show();
            this.Close();
        }
    }
}