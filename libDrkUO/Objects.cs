using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libDrkUO
{
    class Objects
    {
    }
    public class Player : Mobile
    {
        public short HitsCurrent, HitsMax;
        public byte NameChangeFlag, StatusFlag, SexRace;
        public short Str, Dex, Int, StamCurrent, StamMax, ManaCurrent, ManaMax;
        public int Gold;
        public short AR, Weight;

        //Flag 5>
        public short MaxWeight;
        public byte Race;

        //Flag 3>
        public short StatCap;
        public byte Followers, FollowersMax;

        //Flag 4
        public short FireRes, ColdRes, PoisonRes, EnergyRes, Luck, DmgMin, DmgMax;
        public int TithingPoints;

        //Flag 6
        public short HitChanceIncrease, SwingSpeedIncrease, DamageChanceIncrease, LowerReagCost, HitsRegen, StamRegen, ManaRegen, ReflectPhys;
        public short EnhancePotions, DefenseChanceIncrease, SpellDamageIncrease, FasterCastRecovery, FasterCasting, LowerManaCost, StrIncrease, DexIncrease, IntIncrease;
        public short HitsIncrease, StamIncrease, ManaIncrease, MaxHitsIncrease, MaxStamIncrease, MaxManaIncrease;
        private UOProxy.Packets.FromServer._0x1BCharLocaleBody e;
        public Player()
        { }
        public Player(UOProxy.Packets.FromServer._0x1BCharLocaleBody e)
        {
            this.Serial = e.ID;
            this.GraphicID = e.GraphicID;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = e.Z;
            this.Facing = e.Facing;
            
        }
        public Player(UOProxy.Packets.FromServer._0x20DrawGamePlayer e)
        {
            this.Serial = e.Serial;
            this.GraphicID = e.GraphicID;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = e.Z;
            this.Facing = e.Direction;
            this.Flags = e.Flag;
            
        }
    }

    public class Mobile : UOObject
    {
        public string Name = string.Empty;
        public short Str, Dex, Int;
        public short X { get; internal set; }
        public short Y { get; internal set; }
        public byte Z { get; internal set; }

        public byte Facing { get; internal set; }
        public byte Flags { get; internal set; }
        public Mobile()
        { }
        

        public Mobile(UOProxy.Packets.FromServer._0x77UpdatePlayer e)
        {
            this.Serial = e.Serial;
            this.GraphicID = e.GraphicID;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = e.Z;
            this.Facing = e.Direction;
            this.Flags = e.Flags;
            
        }

       
    }

    public class UOObject
    {
        public int Serial { get; internal set; }
        public short GraphicID { get; internal set; }
        public short Hue { get; internal set; }
    }

    public class Item : UOObject
    {


        public short StackSize { get; internal set; }
        public short X { get; internal set; }
        public short Y { get; internal set; }
        public byte Z { get; internal set; }
        
        public int ParentSerial;

        public Item()
        { }

        public Item(UOProxy.Packets.FromServer._0xF3ObjectInfo e)
        {
            this.Serial = e.Serial;
            this.GraphicID = e.GraphicID;
            this.StackSize = e.Amount;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = e.Z;
            this.Hue = e.Hue;
        }

        public Item(UOProxy.Helpers.Item e) // this comes from 0x3c
        {
            this.Serial = e.Serial;
            this.GraphicID = e.GraphicID;
            this.StackSize = e.Amount;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = 0;
            this.ParentSerial = e.ContainerSerial;
            this.Hue = e.Hue;
        }

        public Item(UOProxy.Packets.FromServer._0x2EWornItem e)
        {
            this.Serial = e.Serial;
            this.GraphicID = e.GraphicID;
            this.StackSize = 0;
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
            this.ParentSerial = e.OwnerSerial;
            this.Hue = e.Hue;
        }

        public Item(UOProxy.Packets.FromServer._0x24DrawContainer e)
        {
            this.Serial = e.ID;
            this.GraphicID = e.GraphicID;
            this.StackSize = 0;
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
            
        }

        public Item(UOProxy.Packets.FromServer._0x78DrawObject e)
        {
            this.Serial = e.Serial;
            this.GraphicID = e.GraphicID;
            this.Hue = e.Hue;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = e.Z;
        }

       

    }
}
