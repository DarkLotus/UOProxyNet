﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace UOProxyTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UOProxy.UOProxy proxy = new UOProxy.UOProxy();
            proxy.StartListeningForClient(2593);
            //proxy.EventUpdatePlayer += new UOProxy.UOProxy.UpdatePlayerEventHandler(proxy_EventUpdatePlayer);
            proxy._0x1AObjectInfo +=new UOProxy.UOProxy.ObjectInfoEventHandler(proxy_EventObjectInfo);
            proxy._0x8CConnectToGameServer += new UOProxy.UOProxy.ConnectToGameServerEventHandler(proxy__0x8CConnectToGameServer);
            proxy._0xB0SendGumpMenuDialog += new UOProxy.UOProxy.SendGumpMenuDialogEventHandler(proxy__0xB0SendGumpMenuDialog);
            proxy._0x77UpdatePlayer += new UOProxy.UOProxy.UpdatePlayerEventHandler(proxy__0x77UpdatePlayer);
            proxy.Client_0xB1GumpMenuSelection += new UOProxy.UOProxy.GumpMenuSelectionEventHandler(proxy__0xB1GumpMenuSelection);
            proxy._0xDDCompressedGump += proxy__0xDDCompressedGump;
            proxy.Client_0x80LoginRequest += proxy_Client_0x80LoginRequest;
            int oldcnt = 0;
            while (true)
            {
                Thread.Sleep(5);
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(false);
                    if (key.Key == ConsoleKey.Escape)
                        break;
                }
                if (UOProxy.Logger.MsgLog.Count > oldcnt)
                {
                    lock (UOProxy.Logger.MsgLog)
                    {
                        for(int i = oldcnt;i < UOProxy.Logger.MsgLog.Count;i++)
                        {
                            Console.WriteLine(UOProxy.Logger.MsgLog[i]);
                        }
                        oldcnt = UOProxy.Logger.MsgLog.Count;

                    }

                }
            }
            UOProxy.Logger.SaveLog();
            
        }

        static void proxy_Client_0x80LoginRequest(UOProxy.Packets.FromClient._0x80LoginRequest e)
        {
            string text = BitConverter.ToString(e.Data.ToArray(), 0, (int)e.Data.Length);
            var p = new UOProxy.Packets.FromClient._0x80LoginRequest(e.AccountName, e.Password, e.Key);
            string text2 = BitConverter.ToString(p.Data.ToArray(), 0, (int)p.Data.Length);
            var pp = new UOProxy.Packets.FromClient._0x80LoginRequest(p.Data);
        }

        static void proxy__0xDDCompressedGump(UOProxy.Packets.FromServer._0xDDCompressedGump e)
        {
            Console.WriteLine(e.ToString());
        }

        

        static void proxy__0xB1GumpMenuSelection(UOProxy.Packets.FromClient._0xB1GumpMenuSelection e)
        {
           /* Console.WriteLine(e.ToString());
            string text = BitConverter.ToString(e.Data.ToArray(), 0, (int)e.Data.Length);
            var p = new UOProxy.Packets.FromClient._0xB1GumpMenuSelection(e.GumpID, e.GumpType, e.ButtonID);
            string text2 = BitConverter.ToString(p.Data.ToArray(), 0, (int)p.Data.Length);
            var pp = new UOProxy.Packets.FromClient._0xB1GumpMenuSelection(p.Data);*/
        }

        static void proxy__0x77UpdatePlayer(UOProxy.Packets.FromServer._0x77UpdatePlayer e)
        {
            //Console.WriteLine(e.ToString());
        }

        static void proxy__0xB0SendGumpMenuDialog(UOProxy.Packets.FromServer._0xB0SendGumpMenuDialog e)
        {
            //Console.WriteLine(e.ToString());
        }

        static void proxy__0x8CConnectToGameServer(UOProxy.Packets.FromServer._0x8CConnectToGameServer e)
        {
            // throw new NotImplementedException();
            Console.WriteLine(e.IP.ToString());
        }


        static void proxy_EventConnectToGameServer(UOProxy.Packets.FromServer._0x8CConnectToGameServer e)
        {
            Console.WriteLine(e.IP.ToString());
        }

        static void proxy_EventObjectInfo(UOProxy.Packets.FromServer._0x1AObjectInfo e)
        {
            //Console.WriteLine(e.ToString());
        }

        static void proxy_EventUpdatePlayer(UOProxy.Packets.FromServer._0x77UpdatePlayer e)
        {
            //Console.WriteLine(e.ToString());
        }


    }
}
