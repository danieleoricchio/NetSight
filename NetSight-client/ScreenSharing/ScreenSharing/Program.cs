using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;

namespace ScreenSharing
{
    internal class Program
    {
        static TcpClient client;
        static NetworkStream stream;
        static BinaryFormatter binaryFormatter;
        static ScreenCapture.ScreenCapture screenCapture;
        static public string hostname = "localhost";
        static public int port = 5900, width = 1280, height = 720, screen_number = 0;


        static void Main(string[] args)
        {
            GestioneArgs(args);
            try
            {
                client = new TcpClient(hostname, port);
            }
            catch (IndexOutOfRangeException ex)
            {
                //MessageBox.Show($"Come primo argomento nella riga di comando bisogna mettere l'hostname.");
                Thread.CurrentThread.Interrupt();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Errore: {ex.Message}");
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

        private static void GestioneArgs(string[] args)
        {
            #region Gestione processi
            Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
            for (int i = 0; i < processes.Length - 1; i++)
            {
                processes[i].Kill();
            }
            #endregion
            foreach (var item in args)
            {
                if (item.Contains("?"))
                {
                    //MessageBox.Show("Lista di variabili modificabili:\n - hostname (String)\n - port (int)\n - width (int)\n - height (int)\n - screen_number (int)");
                    Thread.CurrentThread.Interrupt();
                }
                object nomevar = item.Split('=')[0], value = item.Split('=')[1];
                Type type = typeof(Program).GetField(nomevar.ToString()).FieldType;
                typeof(Program).GetField(nomevar.ToString()).SetValue(null, type == typeof(int) ? Convert.ToInt32(value) : value);
                //typeof(Program).GetProperty(nomevar).GetValue(null);
            }
        }


        private static void Start(object thiss)
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
                    //MessageBox.Show($"{ex.Message}\nMi sto chiudendo.");
                    ((Thread)thiss).Interrupt();
                }
                Thread.Sleep(1000 / 30);
            }
        }
    }
}
