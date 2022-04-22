using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
                    Color = Brushes.Gray
                });
            }
            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle r = new Rectangle();
                r.Width = rects[i].Width;
                r.Height = rects[i].Height;
                r.Fill = rects[i].Color;
                r.VerticalAlignment = VerticalAlignment.Top;
                r.HorizontalAlignment = HorizontalAlignment.Left;
                double marginRight = 20 + r.Width * i + (i == 0 ? 0 : 20 * i);
                r.Margin = new Thickness(marginRight, 40, 0, 0);
                myGrid.Children.Add(r);
            }
        }
        struct myRectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public SolidColorBrush Color { get; set; }
        }
    }
}
