using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Client
{
    /// <summary>
    /// Logica di interazione per Registrazione.xaml
    /// </summary>
    public partial class Registrazione : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private JsonMessage? responseRegister;
        public Registrazione()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            responseRegister = new JsonMessage();
        }

        private async void btn_registra_Click(object sender, RoutedEventArgs e)
        {
            string nome = txt_nome.Text;
            string cognome = txt_cognome.Text;
            string mail = txt_mail.Text;
            string data = date.Text;
            string password = txt_password.Text;
            string conferma = txt_conferma.Text;
            if (nome != "" && cognome != "" && mail != "" && data != "" && password != "" && conferma != "")
            {
                if (password == conferma)
                {
                    var values = new Dictionary<string, string>
                    {
                    { "nome", nome },
                    { "cognome", cognome },
                    { "mail", mail },
                    { "data", data },
                    { "password", password }
                    };
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("http://172.16.102.125/Raia/server/confirmRegistration.php", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                    responseRegister = JsonConvert.DeserializeObject<JsonMessage>(responseString);
                    if (responseRegister.result)
                    {
                        MessageBox.Show("Registrazione effettuata");
                        MainApp app = new MainApp(24690);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Registrazione non effettuata");
                    }
                }
                else
                {
                    MessageBox.Show("Inserire due password uguali");
                }
            }
            else
            {
                MessageBox.Show("Inserire tutti i dati");
            }
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