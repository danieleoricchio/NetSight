using Master.Classi;
using System.Windows;
using System.Windows.Media;

namespace Master
{
    /// <summary>
    /// Interaction logic for WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        string type = string.Empty;
        Edificio edificio;
        Laboratorio lab;
        Pc pc;
        public WindowAdd(string Type)
        {
            InitializeComponent();
            this.type = Type;
            switch (type)
            {
                case "edificio":
                    setXamlEdificio();
                    break;
                case "laboratorio":
                    setXamlLaboratorio();
                    break;
                case "pc":
                    lblAdd.Content = "Aggiungi pc";
                    break;
                default:
                    break;
            }
        }

        private void setXamlEdificio()
        {
            lblAdd.Content = "Aggiungi edificio";
            lblAdd.FontFamily = new FontFamily("Arial");
            lblAdd.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl2.Visibility = Visibility.Visible;
            lbl2.Content = "Inserisci indirizzo edificio";
            lbl2.FontFamily = new FontFamily("Arial");
            lbl2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl1.Visibility = Visibility.Visible;
            lbl1.Content = "Inserisci nome edificio";
            lbl1.FontFamily = new FontFamily("Arial");
            lbl1.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            txt1.Visibility = Visibility.Visible;
            txt2.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
        }

        private void setXamlLaboratorio()
        {
            lblAdd.Content = "Aggiungi laboratorio";
            lblAdd.FontFamily = new FontFamily("Arial");
            lblAdd.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl2.Visibility = Visibility.Visible;
            lbl2.Content = "Inserisci codice edificio";
            lbl2.FontFamily = new FontFamily("Arial");
            lbl2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl1.Visibility = Visibility.Visible;
            lbl1.Content = "Inserisci nome laboratorio";
            lbl1.FontFamily = new FontFamily("Arial");
            lbl1.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            txt1.Visibility = Visibility.Visible;
            txt2.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case "edificio":
                    edificio = Edificio.GetEdificio(txt1.Text, txt2.Text);
                    if (edificio.valid)
                    {
                        MessageBox.Show("Edificio aggiunto");
                    }
                    else
                    {
                        MessageBox.Show("Edificio non aggiunto", "Errore nell'aggiunta");
                    }
                    break;
                case "laboratorio":
                    break;
                case "pc":
                    break;
                default:
                    break;
            }
        }
    }
}
