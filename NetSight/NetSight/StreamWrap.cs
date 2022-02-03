using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    class StreamWrap : Stream
    {
        Stream inter;
        public StreamWrap(Stream s)
        {
            inter = s;
        }
        public override bool CanRead => inter.CanRead;

        public override bool CanSeek => inter.CanSeek;

        public override bool CanWrite => inter.CanWrite;

        public override long Length => inter.Length;

        public override long Position { get => inter.Position; set => inter.Position = value; }

        public override void Flush()
        {
            inter.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count <= 0)
                return 0;

            buffer[offset] = (byte)inter.ReadByte();

            return 1;

        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return inter.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            inter.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            inter.Write(buffer, offset, count);
        }
    }

}
