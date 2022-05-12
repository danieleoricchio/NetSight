using Master.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Master
{
    /// <summary>
    /// Logica di interazione per WindowChat.xaml
    /// </summary>
    public partial class WindowChat : Window
    {
        Pc selectedPC;
        string serverIp;
        public WindowChat(Pc pc, string serverIp)
        {
            InitializeComponent();
            this.selectedPC = pc;
            this.serverIp = serverIp;
            lblClient.Content = "Chat con: " + selectedPC.nome;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtMsg.Text == "") { MessageBox.Show("Devi scrivere un messaggio"); return; }

            Messaggio msg = new Messaggio(serverIp, selectedPC.ip, txtMsg.Text);
            Chat chat = new Chat();
            chat.addMsg(msg);
            myChat.Text = "Server >> " + msg.contenuto;
        }
    }
}
