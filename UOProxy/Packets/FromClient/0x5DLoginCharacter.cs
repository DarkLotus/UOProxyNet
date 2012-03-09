using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x5DLoginCharacter : Packet
    {
        public int Pattern1;
        public string Charname;
        public short unknown1;
        public int ClientFlags, unknown2, LoginCount;
        byte[] unknown3 = new byte[16];
        public int Slot;
        public IPAddress ClientIP;

        public _0x5DLoginCharacter(UOStream data) : base(data)
        {
            this.Pattern1 = Data.ReadInt();
            this.Charname = Data.ReadString(30);
            this.unknown1 = Data.ReadShort();
            this.ClientFlags = Data.ReadInt();
            this.unknown2 = Data.ReadInt();
            this.LoginCount = Data.ReadInt();
            this.unknown3 = Data.ReadBytes(16);

            ClientIP = new IPAddress(Data.ReadBytes(4));
            
        }

        public _0x5DLoginCharacter(IPAddress seed, int major, int minor, int rev, int proto)
            : base(0x5D)
        {
            Data.Write(seed.GetAddressBytes(), 0, 4);
            Data.WriteInt(major);
            Data.WriteInt(minor);
            Data.WriteInt(rev);
            Data.WriteInt(proto);
        }
    }
}
