using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UOProxy.Packets.FromClient
{
    public class _0xB1GumpMenuSelection : Packet
    {
        short length;
        public int GumpID, GumpType, ButtonID, SwitchCount, TextCount;
        public List<int> SwitchID = new List<int>();
        public List<short> TextID = new List<short>();
        public List<short> TextLength = new List<short>();
        public List<string> UnicodeText = new List<string>();

         public _0xB1GumpMenuSelection(UOStream Data)
            : base(Data)
            {
             length = Data.ReadShort();

             GumpID = Data.ReadInt();
             GumpType = Data.ReadInt();
             ButtonID = Data.ReadInt();
             SwitchCount = Data.ReadInt();
             if (SwitchCount > 0)
             {
                 for (int i = 0; i <= SwitchCount; i++)
                 {
                     SwitchID.Add(Data.ReadInt());
                 }
             }
             
             TextCount = Data.ReadInt();
             if (TextCount > 0)
             {
                 for (int i = 0; i <= TextCount; i++)
                 {
                     TextID.Add(Data.ReadShort());
                     TextLength.Add(Data.ReadShort());
                     UnicodeText.Add(Data.ReadString(TextLength[i] * 2));
                 }
             }
             

            }
        public _0xB1GumpMenuSelection(int gumpID,int gumpType,int buttonID) : base(0xB1)
         {
             Data.WriteShort(23);//length
             Data.WriteInt(gumpID);
             Data.WriteInt(gumpType);
             Data.WriteInt(buttonID);
             Data.WriteInt(0);//SwitchCount
             Data.WriteInt(0);//TextCount
         }
       
        
    }

    
}
