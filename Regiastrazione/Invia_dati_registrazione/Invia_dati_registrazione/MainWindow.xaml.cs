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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Invia_dati_registrazione
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btn_registra_Click(object sender, RoutedEventArgs e)
        {
            string nome = txt_nome.Text;
            string cognome = txt_cognome.Text;
            string mail = txt_mail.Text;
            string data = date.Text; //splitto e scrivo in forma yyyy/mm/dd
            if (nome != "" && cognome != "" && mail != "" && data != "")
            {
                var values = new Dictionary<string, string>
                {
                    { "nome", nome },
                    { "cognome", cognome },
                    { "mail", mail },
                    { "data", data }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("http://172.16.102.83/Raia/index.php", content);

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);
            }
        }
    }
}
