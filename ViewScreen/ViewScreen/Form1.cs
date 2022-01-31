using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        NetworkStream ns;
        TcpListener listener;
        public Form1()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("C'è già aperta un'instanza di questa applicazione","Errore");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            InitializeComponent();
            //sc = new RemoteDesktopService();
            listener = new TcpListener(5900);
            SetConnessione();
        }
        private void SetConnessione()
        {
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            ns = client.GetStream();
            new Thread(Inizia).Start();
            //Console.WriteLine("Connesso");
            this.Show();
        }
        private void Inizia()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            while (true)
            {
                try
                {
                    Rectangle rectangle = (Rectangle)binaryFormatter.Deserialize(ns);
                    Image _newImage = null;
                    try
                    {
                        _newImage = (Image)binaryFormatter.Deserialize(ns);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Immagine vuota");
                        continue;
                    }
                    
                    //Console.WriteLine("Arrivata image");
                    Image _prevImage = pictureBox1.Image;
                    if (_prevImage != null)
                    {
                        object objectToLock = new object();
                        Graphics graphics;
                        lock (objectToLock)
                        {
                            graphics = Graphics.FromImage(_prevImage);
                        }
                        graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        graphics.DrawImage(_newImage, new Point(rectangle.X, rectangle.Y));
                        pictureBox1.Image = _prevImage;
                        graphics.Dispose();
                    }
                    else
                    {
                        pictureBox1.Image = _newImage;
                    }
                    //pictureBox1.Image = (Image)binaryFormatter.Deserialize(ns);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + $" Alla linea {(new System.Diagnostics.StackTrace(ex, true)).GetFrame(0).GetFileLineNumber()}");
                    listener.Stop();
                    break;
                }
            }
            SetConnessione();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Vuoi chiudere l'applicazione (Si) o chiudere la connessione con il client (No)?", "Chiusura", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    Environment.Exit(0);
                    break;
                case DialogResult.No:
                    this.FormClosing -= Form1_FormClosing;
                    Application.Restart();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
