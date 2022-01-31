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
using System.Runtime.InteropServices;

namespace ScreenSharing
{
    public class Program
    {
        static TcpClient client;
        static NetworkStream stream;
        static BinaryFormatter binaryFormatter;
        static ScreenCapture screenCapture;
        const int SCREEN_NUMBER = 0, WIDTH = 1920, HEIGHT = 1080;
        static public string hostname;
        static public int port;
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                object nomevar = item.Split('=')[0], value = item.Split('=')[1];
                Type type =typeof(Program).GetField(nomevar.ToString()).FieldType;
                if (type== typeof(int))
                {
                    typeof(Program).GetField(nomevar.ToString()).SetValue(null, Convert.ToInt32(value));
                } else
                {
                    typeof(Program).GetField(nomevar.ToString()).SetValue(null, value);
                }
                //typeof(Program).GetProperty(nomevar).GetValue(null);
                
            }
            //MessageBox.Show(s);
            

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

        private static Image GetImage(ref Rectangle rectangle)
        {
            //Rectangle bound = Screen.AllScreens[0].Bounds;
            //Bitmap bitmap = new Bitmap(bound.Width, bound.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //Graphics g = Graphics.FromImage(bitmap);
            //g.CopyFromScreen(bound.X, bound.Y, 0, 0, bound.Size, CopyPixelOperation.SourceCopy);
            //return bitmap;
            return screenCapture.GetScreen(ref rectangle, SCREEN_NUMBER, WIDTH, HEIGHT);
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
                    if (image != null)
                        binaryFormatter.Serialize(stream, image);
                    else
                    {
                        binaryFormatter.Serialize(stream, new object());
                    }
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
