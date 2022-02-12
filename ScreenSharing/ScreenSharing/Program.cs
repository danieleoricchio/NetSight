using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace ScreenSharing
{
    public class Program
    {
        static TcpClient client;
        static NetworkStream stream;
        static BinaryFormatter binaryFormatter;
        static ScreenCapture screenCapture;
        static public string hostname="localhost";
        static public int port=5900, width=1280, height=720, screen_number = 0;

        
        static void Main(string[] args)
        {
            GestioneArgs(args);
            try
            {
                client = new TcpClient(hostname, port);
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show($"Come primo argomento nella riga di comando bisogna mettere l'hostname.");
                Environment.Exit(1);
            } catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}");
                Environment.Exit(1);
            }
            stream = client.GetStream();
            binaryFormatter = new BinaryFormatter();
            screenCapture = new ScreenCapture();
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
                    MessageBox.Show("Lista di variabili modificabili:\n - hostname (String)\n - port (int)\n - width (int)\n - height (int)\n - screen_number (int)");
                    Environment.Exit(0);
                }
                object nomevar = item.Split('=')[0], value = item.Split('=')[1];
                Type type = typeof(Program).GetField(nomevar.ToString()).FieldType;
                typeof(Program).GetField(nomevar.ToString()).SetValue(null, type == typeof(int) ? Convert.ToInt32(value) : value);
                //typeof(Program).GetProperty(nomevar).GetValue(null);
            }
        }

        private static Image GetImage(ref Rectangle rectangle)
        {
            //Rectangle bound = Screen.AllScreens[0].Bounds;
            //Bitmap bitmap = new Bitmap(bound.Width, bound.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //Graphics g = Graphics.FromImage(bitmap);
            //g.CopyFromScreen(bound.X, bound.Y, 0, 0, bound.Size, CopyPixelOperation.SourceCopy);
            //return bitmap;
            return screenCapture.GetScreen(ref rectangle, screen_number, width, height);
        }
        
        
        private static void Start(object thiss)
        {
            while (true)
            {
                try
                {
                    Rectangle rectangle = new Rectangle();
                    Image image = GetImage(ref rectangle);
                    binaryFormatter.Serialize(stream, rectangle);
                    binaryFormatter.Serialize(stream, image != null ? image : new object());
                    Console.WriteLine($"X:{rectangle.X}, Y:{rectangle.Y}, Width:{rectangle.Width}, Height:{rectangle.Height}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\nMi sto chiudendo.");
                    ((Thread)thiss).Abort();
                }
                Thread.Sleep(1000/30);
            }
        }
    }
}
