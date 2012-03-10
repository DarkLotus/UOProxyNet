/*
 * Copyright (C) 2011 - 2012 James Kidd
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UOProxy.Packets;

namespace UOProxy
{
    public partial class UOProxy
    {
        public event MoveRequestEventHandler Client_0x02MoveRequest;
        public delegate void MoveRequestEventHandler(Packets.FromClient._0x02MoveRequest e);

        public event RequestAttackEventHandler Client__0x05RequestAttack;
        public delegate void RequestAttackEventHandler(Packets.FromClient._0x05RequestAttack e);

        public event DoubleClickEventHandler Client_0x06DoubleClick;
        public delegate void DoubleClickEventHandler(Packets.FromClient._0x06DoubleClick e);

        public event PickUpItemEventHandler Client_0x07PickUpItem;
        public delegate void PickUpItemEventHandler(Packets.FromClient._0x07PickUpItem e);

        public event DropItemEventHandler Client_0x08DropItem;
        public delegate void DropItemEventHandler(Packets.FromClient._0x08DropItem e);

        public event SingleClickEventHandler Client_0x09SingleClick;
        public delegate void SingleClickEventHandler(Packets.FromClient._0x09SingleClick e);

        public event LoginCharacterEventHandler Client_0x5DLoginCharacter;
        public delegate void LoginCharacterEventHandler(Packets.FromClient._0x5DLoginCharacter e);

        public event TargetCursorCommandsEventHandler Client_0x6CTargetCursorCommands;
        public delegate void TargetCursorCommandsEventHandler(Packets.FromBoth._0x6CTargetCursorCommands e);

        public event LoginRequestEventHandler Client_0x80LoginRequest;
        public delegate void LoginRequestEventHandler(Packets.FromClient._0x80LoginRequest e);

        public event GameServerLoginEventHandler Client_0x91GameServerLogin;
        public delegate void GameServerLoginEventHandler(Packets.FromClient._0x91GameServerLogin e);

        public event SelectServerEventHandler Client_0xA0SelectServer;
        public delegate void SelectServerEventHandler(Packets.FromClient._0xA0SelectServer e);

        public event GumpMenuSelectionEventHandler Client_0xB1GumpMenuSelection;
        public delegate void GumpMenuSelectionEventHandler(Packets.FromClient._0xB1GumpMenuSelection e);

        public Dictionary<byte, Type> HandlersClient = new Dictionary<byte, Type>();
        private void SetupClientHandlers()
        {
            HandlersClient.Add(0x02, typeof(Packets.FromClient._0x02MoveRequest));
            HandlersClient.Add(0x05, typeof(Packets.FromClient._0x05RequestAttack));
            HandlersClient.Add(0x06, typeof(Packets.FromClient._0x06DoubleClick));
            HandlersClient.Add(0x07, typeof(Packets.FromClient._0x07PickUpItem));
            HandlersClient.Add(0x08, typeof(Packets.FromClient._0x08DropItem));
            HandlersClient.Add(0x09, typeof(Packets.FromClient._0x09SingleClick));
            HandlersClient.Add(0x12, typeof(Packets.FromClient._0x12RequestSkillUse));
            HandlersClient.Add(0x34, typeof(Packets.FromClient._0x34GetPlayerStatus));
            HandlersClient.Add(0x5d, typeof(Packets.FromClient._0x5DLoginCharacter));
            HandlersClient.Add(0x80, typeof(Packets.FromClient._0x80LoginRequest));
            HandlersClient.Add(0x91, typeof(Packets.FromClient._0x91GameServerLogin));
            HandlersClient.Add(0xA0, typeof(Packets.FromClient._0xA0SelectServer));
            HandlersClient.Add(0xB1, typeof(Packets.FromClient._0xB1GumpMenuSelection));
            HandlersClient.Add(0xEC, typeof(Packets.FromClient._0xECEquipMacroKR));
            HandlersClient.Add(0xEF, typeof(Packets.FromClient._0xEFClientLoginSeed));

            HandlersClient.Add(0x6c, typeof(Packets.FromBoth._0x6CTargetCursorCommands));
            HandlersClient.Add(0x73, typeof(Packets.FromBoth._0x73Ping));
            HandlersClient.Add(0xBD, typeof(Packets.FromBoth._0xBDClientVersion));
            HandlersClient.Add(0xC8, typeof(Packets.FromBoth._0xC8ClientViewRange));
            HandlersClient.Add(0xD6, typeof(Packets.FromBoth._0xD6MegaCliloc));
            HandlersClient.Add(0xBF, typeof(Packets.FromBoth._0xBFGeneralInfo));
            
        }
        private void HandleClientPacket(byte[] data, int bytesRead)
        {
            UOStream Data = new UOStream(data);
            Packet packet;
            while (Data.Position < Data.Length - 1)
            {
                if (HandlersClient.ContainsKey(Data.PeekBit()))
                {
                    packet = (Packet)Activator.CreateInstance(HandlersClient[Data.PeekBit()], new object[] { Data });
                    Logger.Log(packet.OpCode.ToString("x") + "  Handled");
                    var eventinfo = this.GetType().GetField("Client" + packet.GetType().Name, BindingFlags.Instance
                        | BindingFlags.NonPublic);

                    if (eventinfo != null)
                    {
                        var member = eventinfo.GetValue(this);
                        if (member != null)
                        {
                            //Logger.Log(member.ToString());
                            member.GetType().GetMethod("Invoke").Invoke(member, new object[] { packet });
                        }
                        else
                        {
                            //Logger.Log("MEMBER WAS NULL FOR EVENT: " + eventinfo.Name);
                        }

                    }
                    else
                    {
                        // Logger.Log("EVENTFIELD WAS NULL FOR PACKET : " + packet.ToString());
                    }
                    /*if (data[0] == 0x8c)
                    { UOProxy.UseHuffman = true; }*/

                    if (Data.Position < Data.Length)
                    {
                        Logger.Log("Buffer contains data after parsing");
                    }
                }
                else
                {
                    Logger.Log(data[0].ToString("x") + "No Client Handler DIScarding");
                    break;
                }
            }

        }
    }
}
