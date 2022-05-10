using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;

namespace Client
{
    public class ScreenSharing
    {
        TcpClient client;
        NetworkStream stream;
        BinaryFormatter binaryFormatter;
        ScreenCapture.ScreenCapture screenCapture;
        public string hostname = "localhost";
        public int port = 5900, width = 1280, height = 720, screen_number = 0;
        public ScreenSharing(string args)
        {
            GestioneArgs(args);
            try
            {
                client = new TcpClient(hostname, port);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}");
                Thread.CurrentThread.Interrupt();
            }
            stream = client.GetStream();
            binaryFormatter = new BinaryFormatter();
            screenCapture = new ScreenCapture.ScreenCapture();
            Thread thread = new Thread(Start);
            thread.Start(thread);
            //RemoteDesktopService rds = new RemoteDesktopService();
            //new Task(() => { Start(rds); }).Start();
        }

        private void GestioneArgs(string arg)
        {
            string[] args = arg.Split(' ');
            foreach (var item in args)
            {
                if (item.Contains("?"))
                {
                    //MessageBox.Show("Lista di variabili modificabili:\n - hostname (String)\n - port (int)\n - width (int)\n - height (int)\n - screen_number (int)");
                    Thread.CurrentThread.Interrupt();
                }
                object nomevar = item.Split('=')[0], value = item.Split('=')[1];
                Type type = typeof(ScreenSharing).GetField(nomevar.ToString()).FieldType;
                typeof(ScreenSharing).GetField(nomevar.ToString()).SetValue(null, type == typeof(int) ? Convert.ToInt32(value) : value);
                //typeof(Program).GetProperty(nomevar).GetValue(null);
            }
        }


        private void Start(object thisThread)
        {
            while (true)
            {
                try
                {
                    Rectangle rectangle = new Rectangle();
                    Image image = screenCapture.GetScreen(ref rectangle, screen_number, width, height);
                    binaryFormatter.Serialize(stream, rectangle);
                    binaryFormatter.Serialize(stream, image != null ? image : new object());
                    Console.WriteLine($"X:{rectangle.X}, Y:{rectangle.Y}, Width:{rectangle.Width}, Height:{rectangle.Height}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\nMi sto chiudendo.");
                    ((Thread)thisThread).Interrupt();
                }
                Thread.Sleep(1000 / 30);
            }
        }
    }
}
