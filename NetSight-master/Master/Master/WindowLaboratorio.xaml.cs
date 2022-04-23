using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        ContextMenu contextMenu;
        bool CanAdd = true;
        public WindowLaboratorio(Laboratorio lab)
        {
            InitializeComponent();
            this.lab = lab;
            contextMenu = new ContextMenu();
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
                r.MouseRightButtonDown += rectangle_MouseRightButtonDown;
            }
        }
        struct myRectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public SolidColorBrush Color { get; set; }
        }

        private void rectangle_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MenuItem item1 = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem item3 = new MenuItem();
            item1.Header = "Condivisione Schermo";
            item2.Header = "Accendi computer";
            item3.Header = "Spegni computer";
            item1.Click += new RoutedEventHandler(menuItem_condSchermo);
            item2.Click += new RoutedEventHandler(menuItem_accendiComputer);
            item3.Click += new RoutedEventHandler(menuItem_spegniComputer);
            if (CanAdd)
            {
                contextMenu.Items.Add(item1);
                contextMenu.Items.Add(item2);
                contextMenu.Items.Add(item3);
                CanAdd = false;
            }
            this.ContextMenu = contextMenu;
        }

        private void menuItem_condSchermo(object sender, RoutedEventArgs e)
        {
            // aprire viewscreen.exe
            // mandare pacchetto per la richeista di condivisione schermo
            MessageBox.Show("Test1");
        }

        private void menuItem_accendiComputer(object sender, RoutedEventArgs e)
        {
            // mandare pacchetto per accendere computer
            MessageBox.Show("Test2");
        }

        private void menuItem_spegniComputer(object sender, RoutedEventArgs e)
        {
            // mandare pacchetto per spegnere computer
            MessageBox.Show("Test3");
        }

    }
}
