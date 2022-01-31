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

namespace NetSight
{
    /// <summary>
    /// Logica di interazione per VisualizzaLaboratorio.xaml
    /// </summary>
    public partial class VisualizzaLaboratorio : Window
    {
        static Laboratorio lab;
        string accesso;
        public VisualizzaLaboratorio()
        {
            InitializeComponent();
            lab = new Laboratorio();
        }
        public VisualizzaLaboratorio(string response)
        {
            accesso = response;
        }
    }
}
