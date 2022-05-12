using Master.Classi;
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
        private string serverIp;
        public WindowLaboratorio(Laboratorio lab, Utente u)
        {
            InitializeComponent();
            Closing += (object? sender, System.ComponentModel.CancelEventArgs e) =>
            {
                foreach (var item in /*lab.listaPc*/rects)
                {
                    sendData("chiusura", item.Pc.ip, 24690);
                }
                Environment.Exit(0);
            };
            this.lab = lab;
            this.user = u;
            Setup();
            threadReceive = new Thread(receivePackets);
            new Thread(checkPcColor).Start();
            aggiornaPc.Click += (object sender, RoutedEventArgs e) => { setPcs(); };
            new Thread(() =>
            {
                while (true)
                {
                    foreach (var item in rects)
                    {
                        try
                        {
                            if (!item.Pc.stato)
                                sendData("riapertura", item.Pc.ip, 24690);
                        }
                        catch (Exception) { }
                    }
                    Thread.Sleep(2000);
                }
            }).Start();
            threadReceive.Start();
            getServerIp();
        }

        private void checkPcColor()
        {
            while (true)
            {
                var pcs = lab.GetPcs();
                for (int i = 0; i < pcs.Count; i++)
                {
                    try
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            try { rects[i].Rectangle.Fill = pcs[i].stato ? green : red; } catch (Exception) { }
                        }));
                    }
                    catch (Exception) { }
                }
                Thread.Sleep(500);
            }
        }

        private void getServerIp()
        {
            string ipaddress = "";
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    ipaddress = endPoint.Address.ToString();
                }
                serverIp = ipaddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
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
            myGrid.Children.Clear();
            rects.Clear();
            int row = 1;
            double marginRight = 0;
            int rectsInRow = 3;
            foreach (Pc item in lab.listaPc)
            {
                myRectangle rectangle = new myRectangle() { Width = 150, Height = 150 };
                rectangle.Pc = item;
                rectangle.Color = item.stato ? green : red;
                rects.Add(rectangle);
                if (item.stato) item.Controllo();
            }
            for (int i = 0; i < rects.Count; i++)
            {
                if (i % rectsInRow == 0 && i != 0)
                {
                    row++;
                    marginRight = 0;
                }
                marginRight += i % rectsInRow == 0 ? 20 : rects[i].Width + 20;
                double marginTop = 40 + (row == 1 ? 0 : (40 + rects[i].Height) * (row - 1));
                Rectangle r = new Rectangle()
                {
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
                Label labelip = new Label()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Width = rects[i].Width,
                    Height = 30,
                    Foreground = Brushes.White,
                    Content = rects[i].Pc.ip,
                    FontFamily = new FontFamily("Arial"),
                    Margin = new Thickness(marginRight, marginTop + rects[i].Height - 30, 0, 0)
                };
                Label labelnome = new Label()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Width = rects[i].Width,
                    Height = 30,
                    Foreground = Brushes.White,
                    Content = rects[i].Pc.nome.ToUpper(),
                    FontFamily = new FontFamily("Arial"),
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
                IPEndPoint receiveEP = new IPEndPoint(IPAddress.Any, 0);

                byte[] dataReceived = udpServer.Receive(ref receiveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                string info = messaggio.Split(";")[0], ip = receiveEP.Address.ToString();
                new Thread(() =>
                {
                    switch (info)
                    {
                        case "alive":
                            Pc pc = lab.GetPc(ip);
                            if (pc == null) return;
                            pc.AggiornaStato(true);
                            break;
                        case "riapertura-confermata":
                            lab.GetPc(ip).AggiornaStato(true);
                            rects[lab.getPos(lab.GetPc(ip))].Pc = lab.GetPc(ip);
                            break;
                        case "chiedi-chiusura":
                            MessageBoxResult result = MessageBox.Show($"{lab.GetPc(ip).nome} ha chiesto la disconnessione.\nAccettare?", "Disconnessione richiesta", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                sendData("chiusura", ip, 24690);
                                lab.GetPc(ip).AggiornaStato(false);
                            }
                            break;
                        default:
                            break;
                    }
                }).Start();
            }
        }
        private int posRectCliccato = -1;
        private void rectangle_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangleCliccato = (Rectangle)sender;
            for (int i = 0; i < rects.Count; i++)
            {
                if (rectangleCliccato == rects[i].Rectangle)
                {
                    posRectCliccato = i;
                }
            }
            ContextMenu contextMenu = new ContextMenu();
            MenuItem item1 = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem item3 = new MenuItem();
            MenuItem item4 = new MenuItem();
            item1.Header = "Condivisione Schermo";
            item2.Header = "Spegni computer";
            item3.Header = "Elimina computer";
            item4.Header = "Apri chat";
            item1.Click += new RoutedEventHandler(menuItem_screenSharing);
            item2.Click += new RoutedEventHandler(menuItem_powerOff);
            item3.Click += new RoutedEventHandler(menuItem_deleteComputer);
            item4.Click += new RoutedEventHandler(menuItem_chat);
            contextMenu.Items.Add(item1);
            contextMenu.Items.Add(item2);
            contextMenu.Items.Add(item3);
            contextMenu.Items.Add(item4);
            this.ContextMenu = contextMenu;
            ContextMenu.Closed += (object sender, RoutedEventArgs e) => { this.ContextMenu = null; };
        }

        private void menuItem_chat(object sender, RoutedEventArgs e)
        {
            WindowChat wc = new WindowChat(rects[posRectCliccato].Pc, serverIp);
            wc.Show();
        }

        private void menuItem_screenSharing(object sender, RoutedEventArgs e)
        {
            if (posRectCliccato <= -1) return;
            Process.Start("ViewScreen.exe");
            sendData("condivisione-schermo", rects[posRectCliccato].Pc.ip, 24690);
        }

        private void menuItem_powerOff(object sender, RoutedEventArgs e)
        {
            //sendData("off", 24690);
        }

        private void menuItem_deleteComputer(object sender, RoutedEventArgs e)
        {
            if (posRectCliccato <= -1) return;
            sendData("chiusura", rects[posRectCliccato].Pc.ip, 24690);
            if (!lab.eliminaPc(posRectCliccato))
            {
                MessageBox.Show("Impossibile eliminare il pc", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            setPcs();
        }

        private void sendData(string dataIn, string ip, int port)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataIn);
            new UdpClient().Send(data, data.Length, ip, port);
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

        private void addPc_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPc windowAddPc = new WindowAddPc(ref lab, user);
            windowAddPc.ShowDialog();
            setPcs();
            
        }

        private void eliminaLab_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sicuro di eliminare questo laboratorio?", "Conferma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                JsonMessage? message = PhpLinkManager.GetMethod<JsonMessage>(PhpLinkManager.URL_delete, new Dictionary<string, string>() { { "type", "laboratorio" }, { "cod", lab.cod.ToString() } });
                if (message == null) { MessageBox.Show("Errore"); return; }
                if (!message.status) { MessageBox.Show("Status false"); return; }
                MessageBox.Show("Laboratorio eliminato", "Operazione completata", MessageBoxButton.OK, MessageBoxImage.Information);
                SceltaLaboratorio scelta = new SceltaLaboratorio(user);
                scelta.Show();
                this.Hide();
            }
        }

        private void lasciaLab_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sicuro di lasicare questo laboratorio?\nDovrai essere reinvitato per entrare a fare parte di questo laboratorio", "Conferma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                JsonMessage? message = PhpLinkManager.GetMethod<JsonMessage>(PhpLinkManager.URL_delete, new Dictionary<string, string>() { { "type", "collegamento-lab" }, { "codlab", lab.cod.ToString() }, { "codadmin", user.cod.ToString() } });
                if (message == null) { MessageBox.Show("Errore"); return; }
                if (!message.status) { MessageBox.Show("Status false"); return; }
                MessageBox.Show("Lasciato questo laboratorio", "Operazione completata", MessageBoxButton.OK, MessageBoxImage.Information);
                SceltaLaboratorio scelta = new SceltaLaboratorio(user);
                scelta.Show();
                this.Hide();
            }
        }
    }
}