using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x06DoubleClick : Packet
    {
        public uint Serial;
        public _0x06DoubleClick(UOStream data) : base(data)
        {
            this.Serial = data.ReadUInt();          
        }

        /*public _0x06DoubleClick(int Serial)
            : base(0x06)
        {
            Data.WriteInt(Serial);
        }*/

        public _0x06DoubleClick(uint Serial)
            : base(0x06)
        {
            Data.WriteUInt(Serial);
        }
    }
}
