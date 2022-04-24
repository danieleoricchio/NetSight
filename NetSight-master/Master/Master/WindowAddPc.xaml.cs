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
    /// Interaction logic for WindowAddPc.xaml
    /// </summary>
    public partial class WindowAddPc : Window
    {
        Laboratorio laboratorio;
        Utente utente;
        public WindowAddPc(Laboratorio lab, Utente user)
        {
            InitializeComponent();
            this.laboratorio = lab;
            this.utente = user;
            Setup();
        }

        private void Setup()
        {
            lbl1.Content = "Inserisci Nome del pc";
            lbl1.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl2.Content = "Inserisci ip del pc";
            lbl2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl3.Content = "Inserisci lo stato del pc";
            lbl3.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl4.Content = "Inserisci il codice del lab";
            lbl4.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void btnIndietroAddPc_Click(object sender, RoutedEventArgs e)
        {
            WindowLaboratorio windowLaboratorio = new WindowLaboratorio(laboratorio, utente);
            windowLaboratorio.Show();
            this.Close();
        }
    }
}
