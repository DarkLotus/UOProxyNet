using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libDrkUO
{
    public partial class libDrkUO
    {

        public ushort CharPosX
        {
            get
            {
                if (Mobiles.ContainsKey(_charID))
                    return (ushort)Mobiles[_charID].X;
                else
                    return 0;
            }
        }
        public ushort CharPosY
        {
            get
            {
                if (Mobiles.ContainsKey(_charID))
                    return (ushort)Mobiles[_charID].Y;
                else
                    return 0;
            }
        }
        public byte CharPosZ
        {
            get
            {
                if (Mobiles.ContainsKey(_charID))
                    return (byte)Mobiles[_charID].Z;
                else
                    return 0;
            }
        }
        public int CharDir
        {
            get
            {
                if (Mobiles.ContainsKey(_charID))
                    return (ushort)Mobiles[_charID].Facing;
                else
                    return 0;
            }
        }
        int _charID;
        public int CharID
        {
            get
            {
                return _charID;
            }
            internal set { _charID = value; }
        }
        public int CharType
        {
            get
            {
                if (Mobiles.ContainsKey(_charID))
                    return (int)Mobiles[_charID].GraphicID;
                else
                    return 0;
            }
        }
        public int CharStatus
        {
            get
            {
                if (Mobiles.ContainsKey(_charID))
                    return (int)Mobiles[_charID].Flags;
                else
                    return 0;
            }
        }
        public int BackpackID
        {
            get
            {
                
                //if (Mobiles.ContainsKey(_charID))
                   // return (int)Mobiles[_charID].GraphicID;
               // else
               //     return 0;
                    return 0;
            }
        }
    }
}
