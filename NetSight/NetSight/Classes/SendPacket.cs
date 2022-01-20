using System.Net.Sockets;
using System.Text;

namespace NetSight.Classes
{
    static class SendPacket
    {
        static UdpClient client = new UdpClient();

        public static void Send(string azione, string ip)
        {
            if (azione != string.Empty)
            {
                byte[] data = Encoding.ASCII.GetBytes(azione + ";");
                client.Send(data, data.Length, ip, 12345);
            }
        }
    }
}
