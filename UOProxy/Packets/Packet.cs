using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UOProxy.Packets
{
    public class Packet
    {
        public UOStream Data;
        public byte OpCode;
        public Type PacketType;
        public Packet(UOStream data)
        {
            this.Data = data;
            this.OpCode = this.Data.ReadBit();
        }
        public Packet()
        {
            Data = new UOStream();
        }
        public Packet(byte OpCode)
        {
            Data = new UOStream();
            Data.WriteByte(OpCode);
        }
        public byte[] PacketData { get { return Data.ToArray(); } }

    }
}
