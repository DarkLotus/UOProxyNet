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
        public List<string> TextToAdd = new List<string>();

        public _0xD6MegaCliloc(UOStream Data)
            : base(Data)
        {
            OpenUO.Ultima.UnicodeFontFactory<string> factory = new OpenUO.Ultima.UnicodeFontFactory<string>(OpenUO.Ultima.InstallationLocator.Locate().FirstOrDefault());
            length = Data.ReadShort();
            unknown1 = Data.ReadShort();
            Serial = Data.ReadInt();
            unknown2 = Data.ReadShort();
            OwnerID = Data.ReadInt();
            while(Data.Position + 6 < Data.Length)
            {
                ClilocIDs.Add(Data.ReadInt());
                TextToAdd.Add(Data.ReadString(Data.ReadShort()));
                
            }
                  //TODO FINISH THIS     
            
        }
    }
}
