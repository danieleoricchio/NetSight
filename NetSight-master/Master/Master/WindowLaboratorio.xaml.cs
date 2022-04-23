using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        private Thread threadRicevi;
        public WindowLaboratorio(Laboratorio lab)
        {
            InitializeComponent();
            Closing += (object? sender, System.ComponentModel.CancelEventArgs e) => { Environment.Exit(0); };
            this.lab = lab;
            Setup();
            threadRicevi = new Thread(riceviPacchetti);
            threadRicevi.Start();
            new Thread(controlloColoriPc).Start();
        }

        private void controlloColoriPc()
        {
            while (true)
            {
                foreach (Pc item in lab.listaPc)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { ((Rectangle)myGrid.Children[item.cod - 1]).Fill = item.stato ? verde : rosso; }));
                }
                Thread.Sleep(500);
            }
        }

        private void Setup()
        {
            lblLab.Content = "Laboratorio: " + lab.nome;
            rects = new List<myRectangle>();
            setPcs();
        }

        private void setPcs()
        {
            int riga = 1;
            double marginRight = 0;
            int quantiRettangoliInUnaRiga = 6;
            foreach (Pc item in lab.listaPc)
            {
                myRectangle rectangle = standardRectangle;
                rectangle.Color = !item.stato ? rosso : verde ;
                rects.Add(rectangle);
                item.Controllo();
            }
            for (int i = 0; i < rects.Count; i++)
            {
                if (i % quantiRettangoliInUnaRiga == 0 && i != 0)
                {
                    riga++;
                    marginRight = 0;
                }
                Rectangle r = new Rectangle();
                r.Width = rects[i].Width;
                r.Height = rects[i].Height;
                r.Fill = rects[i].Color;
                r.VerticalAlignment = VerticalAlignment.Top;
                r.HorizontalAlignment = HorizontalAlignment.Left;
                marginRight += (i % quantiRettangoliInUnaRiga == 0 ? 20 : r.Width + 20);
                double marginTop = 40 + (riga == 1 ? 0 : (40+r.Height) * (riga-1));
                r.Margin = new Thickness(marginRight, marginTop, 0, 0);
                r.MouseRightButtonUp += rectangle_MouseRightButtonUp;
                myGrid.Children.Add(r);
            }
        }
        private void riceviPacchetti()
        {
            UdpClient udpServer = new UdpClient(25000);
            while (true)
            {
                IPEndPoint riceiveEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataReceived = udpServer.Receive(ref riceiveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                string info = messaggio.Split(";")[0], ip = messaggio.Split(";")[1];
                new Thread(() =>
                {
                    switch (info)
                    {
                        case "alive":
                            Pc pc = lab.GetPc(ip);
                            pc.AggiornaStato(true);
                            break;
                        case "connected":
                            Pc pc1 = new Pc(true);
                            //pc.ip = txtIpPc.Text.ToString();
                            //pc.nome = txtNomePc.Text.ToString();
                            //labs.Add(pc);
                            break;
                        default:
                            break;
                    }
                }).Start();
            }
        }
        private void rectangle_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem item1 = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem item3 = new MenuItem();
            item1.Header = "Condivisione Schermo";
            item2.Header = "Accendi computer";
            item3.Header = "Spegni computer";
            item1.Click += new RoutedEventHandler(menuItem_condSchermo);
            item2.Click += new RoutedEventHandler(menuItem_accendiComputer);
            item3.Click += new RoutedEventHandler(menuItem_spegniComputer);
            contextMenu.Items.Add(sender.ToString());
            contextMenu.Items.Add(item1);
            contextMenu.Items.Add(item2);
            contextMenu.Items.Add(item3);
            this.ContextMenu = contextMenu;
            ContextMenu.Closed += (object sender, RoutedEventArgs e) => { this.ContextMenu = null; };
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


        public struct myRectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public SolidColorBrush Color { get; set; }
        }
        static public myRectangle standardRectangle = new myRectangle() { Width = 150, Height = 150, Color = Brushes.White };
        static private SolidColorBrush verde = new SolidColorBrush(Color.FromRgb(125, 255, 125)), rosso = new SolidColorBrush(Color.FromRgb(255, 125, 125));
    }
}
