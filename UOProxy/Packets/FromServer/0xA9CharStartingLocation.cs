using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xA9CharStartingLocation : Packet
    {
        short _length;
        public byte NumberOfChars;
        
        public Dictionary<byte,string> Characters = new Dictionary<byte,string>();
        public byte NumberStartingCities;

        public _0xA9CharStartingLocation(UOStream Data)
            : base(Data)
        {
            _length = Data.ReadShort();
            NumberOfChars = Data.ReadBit();
            for (int i = 0; i < NumberOfChars; i++)
            { 
                Characters.Add((byte)(i+1),Data.ReadString(30));
            }
            NumberStartingCities = Data.ReadBit();
        }
    }


}
