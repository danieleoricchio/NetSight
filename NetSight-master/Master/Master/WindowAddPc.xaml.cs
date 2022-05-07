using Master.Classi;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Master
{
    /// <summary>
    /// Interaction logic for WindowAddPc.xaml
    /// </summary>
    public partial class WindowAddPc : Window
    {
        Laboratorio laboratorio;
        Utente utente;
        public WindowAddPc(ref Laboratorio lab, Utente user)
        {
            InitializeComponent();
            this.laboratorio = lab;
            this.utente = user;
            SetupXaml();
        }

        private void SetupXaml()
        {
            lbl1.Content = "Inserisci Nome del pc";
            lbl1.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl2.Content = "Inserisci ip del pc";
            lbl2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void btnIndietroAddPc_Click(object sender, RoutedEventArgs e)
        {
            WindowLaboratorio windowLaboratorio = new WindowLaboratorio(laboratorio, utente);
            windowLaboratorio.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool timerScaduto = false;
            System.Timers.Timer timer = new System.Timers.Timer(100000);
            timer.Elapsed += (object? sender, System.Timers.ElapsedEventArgs e) => { timerScaduto = true; };
            if (txt1.Text == string.Empty && txt2.Text == string.Empty) { MessageBox.Show("Inserire i dati correttamente"); return; }

            sendData("apertura", 24690, txt2.Text.Trim());
            UdpClient udpClient = new UdpClient(24690);
            timer.Start();
            do
            {
                IPEndPoint riceiveEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataReceived = udpClient.Receive(ref riceiveEP);
                string messaggio = Encoding.ASCII.GetString(dataReceived);
                if (messaggio.Equals("apertura-confermata") && riceiveEP.Address.ToString().Equals(txt2.Text.Trim()))
                {
                    break;
                }
            } while (!timerScaduto);
            timer.Stop();
            if (timerScaduto)
            {
                MessageBox.Show($"Il pc ({txt2.Text.Trim()}) non risponde. \nMagari l'indirizzo ip è sbagliato?", "Impossibile contattare " + txt2.Text.Trim(), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Dictionary<string, string> valuesLab = new Dictionary<string, string>()
            {
                  { "nome", txt1.Text.Trim() },
                  { "ip", txt2.Text.Trim() },
                  { "codlab", laboratorio.cod.ToString() },
                  { "type", "pc" }
            };
            JsonMessage? messagePC = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_add, valuesLab);
            if (messagePC == null)
            {
                MessageBox.Show("PC non aggiunto", "Errore nell'aggiunta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!messagePC.status)
            {
                MessageBox.Show("PC non aggiunto. " + messagePC.message, "Errore nell'aggiunta", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            laboratorio.addPc(new Pc(true, txt1.Text.Trim(), txt2.Text.Trim()));
            MessageBox.Show("PC aggiunto");
        }

        private void sendData(string dataIn, int port, string ip)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataIn);
            new UdpClient().Send(data, data.Length, ip, port);
        }
    }
}
