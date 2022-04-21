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

namespace Master
{
    /// <summary>
    /// Interaction logic for WindowLaboratorio.xaml
    /// </summary>
    public partial class WindowLaboratorio : Window
    {
        private Laboratorio lab;
        private List<myRectangle> rects;
        public WindowLaboratorio(Laboratorio lab)
        {
            InitializeComponent();
            this.lab = lab;
            Setup();
        }

        private void Setup()
        {
            lblLab.Content = "Laboratorio: " + lab.nome;
            rects = new List<myRectangle>();
            setPcs();
        }

        private void setPcs()
        {
            for (int i = 0; i < lab.GetPcs().Count; i++)
            {
                rects.Add(new myRectangle()
                {
                    Width = 100,
                    Height = 100,
                    Left = 0,
                    Top = 0,
                    Color = Brushes.Red
                });
            }

            foreach (myRectangle rect in rects)
            {
                MessageBox.Show("Rettangoli count: " + rects.Count.ToString());
                Rectangle r = new Rectangle();
                r.Width = rect.Width;
                r.Height = rect.Height;
                r.Fill = rect.Color;

                Canvas.SetLeft(r, rect.Left);
                Canvas.SetTop(r, rect.Top);

                myCanvas.Children.Add(r);
            }

        }
        struct myRectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public int Left { get; set; }
            public int Top { get; set; }
            public SolidColorBrush Color { get; set; }
        }
    }
}
