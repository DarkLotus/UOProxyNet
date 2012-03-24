using System;
using System.Threading;

namespace libDrkUO
{
    public partial class libDrkUO
    {
        void proxy__0xF3ObjectInfo(UOProxy.Packets.FromServer._0xF3ObjectInfo e)
        {
            if (!Objects.ContainsKey(e.Serial))
                Objects.Add(e.Serial, new Item(e));
            else
                Objects[e.Serial] = new Item(e);
        }

        void proxy__0xDDCompressedGump(UOProxy.Packets.FromServer._0xDDCompressedGump e)
        {
            //throw new NotImplementedException();
        }

        void proxy__0xD6MegaCliloc(UOProxy.Packets.FromBoth._0xD6MegaCliloc e)
        {
            throw new NotImplementedException();
        }

        void proxy__0xC1ClilocMessage(UOProxy.Packets.FromServer._0xC1ClilocMessage e)
        {
            throw new NotImplementedException();
        }

        void proxy__0x78DrawObject(UOProxy.Packets.FromServer._0x78DrawObject e)
        {
            if (!Objects.ContainsKey(e.Serial))
                Objects.Add(e.Serial, new Item(e));
            else
                Objects[e.Serial] = new Item(e);
            
        }

        void proxy__0x77UpdatePlayer(UOProxy.Packets.FromServer._0x77UpdatePlayer e)
        {
            if (Mobiles.ContainsKey(e.Serial))
            {
                Mobiles[e.Serial] = new Mobile(e);
            }
            else { Mobiles.Add(e.Serial, new Mobile(e)); }
        }

        void proxy__0x6CTargetCursorCommands(UOProxy.Packets.FromBoth._0x6CTargetCursorCommands e)
        {
            throw new NotImplementedException();
        }

        void proxy__0x3CAddMultipleItemsToContainer(UOProxy.Packets.FromServer._0x3CAddMultipleItemsToContainer e)
        {
            foreach(var i in e.Items)
            {
                if (!Objects.ContainsKey(i.Serial))
                    Objects.Add(i.Serial, new Item(i));
                else
                    Objects[i.Serial] = new Item(i);
            }
            
        }

        void proxy__0x2EWornItem(UOProxy.Packets.FromServer._0x2EWornItem e)
        {
            if (!Objects.ContainsKey(e.Serial))
                Objects.Add(e.Serial, new Item(e));
            else
                Objects[e.Serial] = new Item(e);
        }

        void proxy__0x2DMobAttributes(UOProxy.Packets.FromServer._0x2DMobAttributes e)
        {
            throw new NotImplementedException();
        }

        void proxy__0x25AddItemToContainer(UOProxy.Packets.FromServer._0x25AddItemToContainer e)
        {
            throw new NotImplementedException();
        }

        void proxy__0x24DrawContainer(UOProxy.Packets.FromServer._0x24DrawContainer e)
        {
            if (!Objects.ContainsKey(e.ID))
                Objects.Add(e.ID, new Item(e));
            else
                Objects[e.ID] = new Item(e);
        }

        void proxy__0x22MoveAck(UOProxy.Packets.FromBoth._0x22MoveAck e)
        {
            throw new NotImplementedException();
        }

        void proxy__0x21CharMoveRejection(UOProxy.Packets.FromServer._0x21CharMoveRejection e)
        {
            throw new NotImplementedException();
        }

        void proxy__0x20DrawGamePlayer(UOProxy.Packets.FromServer._0x20DrawGamePlayer e)
        {
            
            if (!Mobiles.ContainsKey(e.Serial))
                Mobiles.Add(e.Serial, new Player(e));
            else
                Mobiles[e.Serial] = new Player(e);
        }

        void proxy__0x1DDeleteObject(UOProxy.Packets.FromServer._0x1DDeleteObject e)
        {
            lock (Objects)
            {
            if(Objects.ContainsKey(e.ID))
                Objects.Remove(e.ID);
            }
        }

        void proxy__0x1CSendSpeech(UOProxy.Packets.FromServer._0x1CSendSpeech e)
        {
            throw new NotImplementedException();
        }

        void proxy__0x16StatusBarUpdate(UOProxy.Packets.FromServer._0x16StatusBarUpdate e)
        {
            throw new NotImplementedException();
            
        }

        void proxy__0x11StatusBarInfo(UOProxy.Packets.FromServer._0x11StatusBarInfo e)
        {
            if (Mobiles.ContainsKey(e.PlayerID))
            {
                Player p = (Player)Mobiles[e.PlayerID];
                p.Str = e.Str;
                p.Dex = e.Dex;
                p.Int = e.Int;
                p.AR = e.AR;
                p.ColdRes = e.ColdRes;
                p.DamageChanceIncrease = e.DamageChanceIncrease;
                p.DefenseChanceIncrease = e.DefenseChanceIncrease;
                p.DexIncrease = e.DexIncrease;
                p.DmgMax = e.DmgMax;
                p.DmgMin = e.DmgMin;
                p.EnergyRes = e.EnergyRes;
                p.EnhancePotions = e.EnhancePotions;
                p.FasterCasting = e.FasterCasting;
                p.FasterCastRecovery = e.FasterCastRecovery;
                p.FireRes = e.FireRes;
                p.Followers = e.Followers;
                p.FollowersMax = e.FollowersMax;
                p.Gold = e.Gold;
                p.HitChanceIncrease = e.HitChanceIncrease;
                p.HitsCurrent = e.HitsCurrent;
                p.HitsIncrease = e.HitsIncrease;
                p.HitsMax = e.HitsMax;
                p.HitsRegen = e.HitsRegen;
                p.IntIncrease = e.IntIncrease;
                p.LowerManaCost = e.LowerManaCost;
                p.LowerReagCost = e.LowerReagCost;
                p.Luck = e.Luck;
                p.ManaCurrent = e.ManaCurrent;
                p.ManaIncrease = e.ManaIncrease;
                p.ManaMax = e.ManaMax;
                p.ManaRegen = e.ManaRegen;
                p.MaxHitsIncrease = e.MaxHitsIncrease;
                p.MaxManaIncrease = e.MaxManaIncrease;
                p.MaxStamIncrease = e.MaxStamIncrease;
                p.MaxWeight = e.MaxWeight;
                p.Name = e.PlayerName;
                p.PoisonRes = e.PoisonRes;
                p.Race = e.Race;
                p.ReflectPhys = e.ReflectPhys;
                p.SexRace = e.SexRace;
                p.SpellDamageIncrease = e.SpellDamageIncrease;
                p.StamCurrent = e.StamCurrent;
                p.StamIncrease = e.StamIncrease;
                p.StamMax = e.StamMax;
                p.StamRegen = e.StamRegen;
                p.StatCap = e.StatCap;
                p.StatusFlag = e.StatusFlag;
                p.StrIncrease = e.StrIncrease;
                p.SwingSpeedIncrease = e.SwingSpeedIncrease;
                p.TithingPoints = e.TithingPoints;

            }
            else
            {
            // Player not found 
                throw new Exception();
            }

        }

        void proxy__0x0BDamage(UOProxy.Packets.FromServer._0x0BDamage e)
        {
            throw new NotImplementedException();
        }


        void proxy__0xA9CharStartingLocation(UOProxy.Packets.FromServer._0xA9CharStartingLocation e)
        {
            var p = new UOProxy.Packets.FromClient._0x5DLoginCharacter(e.Characters[1], 0);
            proxy.SendToServer(p);
        }

        void proxy__0xA8GameServerList(UOProxy.Packets.FromServer._0xA8GameServerList e)
        {
            var packet = new UOProxy.Packets.FromClient._0xA0SelectServer(0);
            proxy.SendToServer(packet);
        }

        void proxy__0xBDClientVersion(UOProxy.Packets.FromBoth._0xBDClientVersion e)
        {
            var p = new UOProxy.Packets.FromBoth._0xBDClientVersion("7.0.23.1");
            proxy.SendToServer(p);


        }

        void proxy__0x73Ping(UOProxy.Packets.FromBoth._0x73Ping e)
        {
            proxy.SendToServer(e);
        }


        void proxy__0x8CConnectToGameServer(UOProxy.Packets.FromServer._0x8CConnectToGameServer e)
        {
            Keyy = e.Key;

            Thread.Sleep(100);
            tcpserver = proxy.ConnectToServer(server, port);
            UOProxy.UOProxy.UseHuffman = true;

            byte[] _buffer = new byte[4];
            _buffer[0] = (byte)(Keyy >> 24);
            _buffer[1] = (byte)(Keyy >> 16);
            _buffer[2] = (byte)(Keyy >> 8);
            _buffer[3] = (byte)Keyy;
            proxy.SendToServer(_buffer);
            proxy.SendToServer(new UOProxy.Packets.FromClient._0x91GameServerLogin(Keyy, "infotech1", "junk2"));
            Keyy = 0;


        }

        void proxy__0x55LoginComplete(UOProxy.Packets.FromServer._0x55LoginComplete e)
        {
            _loggedIn = true;
        }

        void proxy__0x1BCharLocaleBody(UOProxy.Packets.FromServer._0x1BCharLocaleBody e)
        {
            if (Mobiles.ContainsKey(e.ID))
            {
                Mobiles[e.ID] = new Player(e);
            }
            else
                Mobiles.Add(e.ID, new Player(e));
            this._charID = e.ID;
        }
    }
}
