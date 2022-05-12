using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Classi
{
    public class Chat
    {
        public List<Messaggio> chat;

        public Chat()
        {
            chat = new List<Messaggio>();
        }

        public void addMsg(Messaggio msg)
        {
            chat.Add(msg);
        }
    }
}
