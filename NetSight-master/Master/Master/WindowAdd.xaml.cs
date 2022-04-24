using Master.Classi;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Master
{
    /// <summary>
    /// Interaction logic for WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        string type = string.Empty;
        Edificio edificio;
        public WindowAdd(string Type)
        {
            InitializeComponent();
            this.type = Type;
            switch (type)
            {
                case "edificio":
                    lblAdd.Content = "Aggiungi edificio";
                    lblIndirizzo.Visibility = Visibility.Visible;
                    lblNome.Visibility = Visibility.Visible;
                    txtNome.Visibility = Visibility.Visible;
                    txtIndirizzo.Visibility = Visibility.Visible;
                    btnAdd.Visibility = Visibility.Visible;
                    break;
                case "laboratorio":
                    lblAdd.Content = "Aggiungi laboratorio";
                    break;
                case "pc":
                    lblAdd.Content = "Aggiungi pc";
                    break;
                default:
                    break;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            edificio = Edificio.GetEdificio(txtNome.Text, txtIndirizzo.Text);
            if (edificio.valid)
            {
                MessageBox.Show("Edificio aggiunto", "Errore nell'aggiunta");

            }
            else
            {
                MessageBox.Show("Edificio non aggiunto", "Errore nell'aggiunta");
            }
        }
    }
}
