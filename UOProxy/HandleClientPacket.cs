﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UOProxy.Packets;

namespace UOProxy
{
    public partial class UOProxy
    {
        public event RequestAttackEventHandler Client__0x05RequestAttack;
        public delegate void RequestAttackEventHandler(Packets.FromClient._0x05RequestAttack e);

        public event DoubleClickEventHandler Client_0x06DoubleClick;
        public delegate void DoubleClickEventHandler(Packets.FromClient._0x06DoubleClick e);

        public event PickUpItemEventHandler Client_0x07PickUpItem;
        public delegate void PickUpItemEventHandler(Packets.FromClient._0x07PickUpItem e);

        public event GumpMenuSelectionEventHandler Client_0xB1GumpMenuSelection;
        public delegate void GumpMenuSelectionEventHandler(Packets.FromClient._0xB1GumpMenuSelection e);

        public Dictionary<byte, Type> HandlersClient = new Dictionary<byte, Type>();
        private void SetupClientHandlers()
        {
            HandlersClient.Add(0x05, typeof(Packets.FromClient._0x05RequestAttack));
            HandlersClient.Add(0x06, typeof(Packets.FromClient._0x06DoubleClick));
            HandlersClient.Add(0x07, typeof(Packets.FromClient._0x07PickUpItem));
            HandlersClient.Add(0xB1, typeof(Packets.FromClient._0xB1GumpMenuSelection));
            
        }
        private void HandleClientPacket(byte[] data, int bytesRead)
        {
            UOStream Data = new UOStream(data);
            Packet packet;
            if (HandlersClient.ContainsKey(data[0]))
            {
                packet = (Packet)Activator.CreateInstance(HandlersClient[data[0]], new object[] { Data });
                //Logger.Log(packet.ToString() + "Handled");
                var eventinfo = this.GetType().GetField("Client" + packet.GetType().Name, BindingFlags.Instance
                    | BindingFlags.NonPublic);

                if (eventinfo != null)
                {
                    var member = eventinfo.GetValue(this);
                    if (member != null)
                    {
                        Logger.Log(member.ToString());
                        member.GetType().GetMethod("Invoke").Invoke(member, new object[] { packet });
                    }
                    else
                    {
                        //Logger.Log("MEMBER WAS NULL FOR EVENT: " + eventinfo.Name);
                    }

                }
                else
                {
                    Logger.Log("EVENTFIELD WAS NULL FOR PACKET : " + packet.ToString());
                }
                if (data[0] == 0x8c)
                { UOProxy.UseHuffman = true; }
                return;
            }
            else
            {
                Logger.Log(data[0].ToString("x") + "No Client Handler");
                
            }
        }
    }
}