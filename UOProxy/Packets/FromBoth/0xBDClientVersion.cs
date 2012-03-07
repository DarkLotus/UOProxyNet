using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0xBDClientVersion : Packet
    {
        short _len;
        public string Version;
        public _0xBDClientVersion(UOStream Data)
            : base(Data)
        {
            _len = Data.ReadShort();
            Version = Data.ReadNullTermString();
        }
        public _0xBDClientVersion(string version)
            : base(0x22)
        {
            byte[] ms = System.Text.Encoding.UTF8.GetBytes(version);
            Data.WriteShort((short)(ms.Length + 3));
            Data.WriteString(version, ms.Length + 2);
        }
    }
}
