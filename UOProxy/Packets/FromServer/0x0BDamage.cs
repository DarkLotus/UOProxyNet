using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x0BDamage : Packet
    {
        public int Serial;
        public short DamageDealt;
        public _0x0BDamage(UOStream Data)
            : base(Data)
        {
            Serial = Data.ReadInt();
            DamageDealt = Data.ReadShort();
            
        }
    }
}
