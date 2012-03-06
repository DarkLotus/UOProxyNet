using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xD6MegaCliloc : Packet
    {
        short length, unknown1;
        public int Serial;
        short unknown2;
        public int OwnerID;
        public List<int> ClilocIDs = new List<int>();
        short lengthoftext;
        //public List<string> TextToAdd = new List<string>();

        public _0xD6MegaCliloc(UOStream Data)
            : base(Data)
        {


            length = Data.ReadShort();
            unknown1 = Data.ReadShort();
            Serial = Data.ReadInt();
            unknown2 = Data.ReadShort();
            OwnerID = Data.ReadInt();
            List<string> Cliocs = new List<string>();
            while(Data.Position + 6 < Data.Length)
            {
                int MessageNumber = Data.ReadInt();
                short textlen = Data.ReadShort();
                if (textlen + Data.Position > Data.Length)
                {
                    int errorrr = 1;
                    break;
                }

                if(textlen > 0)
                {
                    string _args = Data.ReadUnicodeStringLE(textlen);
                    Cliocs.Add(Helpers.Cliloc.constructCliLoc(Helpers.Cliloc.Table[MessageNumber].ToString(), _args));
                }
                   
                Cliocs.Add(Helpers.Cliloc.Table[MessageNumber].ToString());
            }
                  //TODO FINISH THIS     
            
        }
    }
}
