using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromBoth
{
    public class _0xBFGeneralInfo : Packet
    {
        public byte Seq, Notoriety;
        public _0xBFGeneralInfo(UOStream Data)
            : base(Data)
        {
            Seq = Data.ReadBit();
            Notoriety = Data.ReadBit();         
        }
        public _0xBFGeneralInfo(byte seq, byte notor)
            : base(0xBF)
        {
            this.Data.WriteBit(seq);
            this.Data.WriteBit(notor);
        }
    }
}
//TODO 