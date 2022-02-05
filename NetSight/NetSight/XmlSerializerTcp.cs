using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NetSight
{
    public class XmlSerializerTcp
    {
     

        XmlSerializer inter;
        Type t;
        public XmlSerializerTcp(Type t)
        {
            inter = new XmlSerializer(t);
            this.t = t;
        }
        public void Serialize(NetworkStream stream, object o)
        {
            inter.Serialize(stream, o);
            stream.WriteByte(10);
            stream.WriteByte((byte)'<');
        }


        public object Deserialize(NetworkStream stream)
        {
            using (XmlReaderTcp reader = new XmlReaderTcp(stream))
            {
                return inter.Deserialize(reader);
            }
        }

       
    }
}
