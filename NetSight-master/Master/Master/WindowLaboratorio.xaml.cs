using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Thread threadReceive;
        Utente user;
        public WindowLaboratorio(Laboratorio lab, Utente u)
        {
            InitializeComponent();
            Closing += (object? sender, System.ComponentModel.CancelEventArgs e) => { Environment.Exit(0); };
            this.lab = lab;
            this.user = u;
            Setup();
            threadReceive = new Thread(receivePackets);
            threadReceive.Start();
            new Thread(checkPcColor).Start();
        }

        private void checkPcColor()
        {
            while (true)
            {
                foreach (Pc item in lab.listaPc)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { rects[lab.getPos(item)].Rectangle.Fill = item.stato ? green : red; }));
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
            int row = 1;
            double marginRight = 0;
            int rectsInRow = 6;
            foreach (Pc item in lab.listaPc)
            {
                myRectangle rectangle = new myRectangle() { Width = 150, Height = 150};
                rectangle.Pc = item;
                rectangle.Color = !item.stato ? green : red;
                rects.Add(rectangle);
                item.Controllo();
            }
            for (int i = 0; i < rects.Count; i++)
            {
                if (i % rectsInRow == 0 && i != 0)
                {
                    row++;
                    marginRight = 0;
                }
                marginRight += (i % rectsInRow == 0 ? 20 : rects[i].Width + 20);
                double marginTop = 40 + (row == 1 ? 0 : (40 + rects[i].Height) * (row - 1));
                Rectangle r = new Rectangle() {
                    Width = rects[i].Width,
                    Height = rects[i].Height,
                    Fill = rects[i].Color,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(marginRight, marginTop, 0, 0),
                    RadiusX = 20,
                    RadiusY = 20,
                };
                r.MouseRightButtonUp += rectangle_MouseRightButtonUp;
                Label labelip = new Label() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Width = rects[i].Width,
                    Height = 30,
                    Foreground = Brushes.Black,
                    Content = rects[i].Pc.ip,
                    Margin = new Thickness(marginRight, marginTop + rects[i].Height - 30, 0, 0) 
                };
                Label labelnome = new Label()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Width = rects[i].Width,
                    Height = 30,
                    Foreground = Brushes.Black,
                    Content = rects[i].Pc.nome,
                    Margin = new Thickness(marginRight, marginTop - 25, 0, 0)
                };
                rects[i].Rectangle = r;
                myGrid.Children.Add(r);
                myGrid.Children.Add(labelip);
                myGrid.Children.Add(labelnome);
            }
        }
        private void receivePackets()
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
            item1.Click += new RoutedEventHandler(menuItem_screenSharing);
            item2.Click += new RoutedEventHandler(menuItem_powerOn);
            item3.Click += new RoutedEventHandler(menuItem_powerOff);
            contextMenu.Items.Add(item1);
            contextMenu.Items.Add(item2);
            contextMenu.Items.Add(item3);
            this.ContextMenu = contextMenu;
            ContextMenu.Closed += (object sender, RoutedEventArgs e) => { this.ContextMenu = null; };
        }

        private void menuItem_screenSharing(object sender, RoutedEventArgs e)
        {
            Process.Start("ViewScreen.exe");
            sendData("condivisione-schermo", 24690);
        }

        private void menuItem_powerOn(object sender, RoutedEventArgs e)
        {
            sendData("apertura", 24690);
        }

        private void menuItem_powerOff(object sender, RoutedEventArgs e)
        {
            sendData("off", 24690);
        }

        private void sendData(string dataIn, int port)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataIn);
            new UdpClient().Send(data, data.Length, lab.listaPc[0].ip, port);
        }


        public class myRectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public SolidColorBrush Color { get; set; }
            public Pc Pc { get; set; }
            public Rectangle Rectangle { get; set; }
        }

        static public myRectangle standardRectangle = new myRectangle() { Width = 150, Height = 150, Color = Brushes.White };
        static private SolidColorBrush green = new SolidColorBrush(Color.FromRgb(125, 255, 125)), red = new SolidColorBrush(Color.FromRgb(255, 125, 125));

        //private void btnIndietroPage2_Click(object sender, RoutedEventArgs e)
        //{
        //    SceltaLaboratorio sceltaLaboratorio = new SceltaLaboratorio(user);
        //    sceltaLaboratorio.Show();
        //    this.Close();
        //}
    }
}