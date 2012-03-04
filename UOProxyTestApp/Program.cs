using System;
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
            while (true)
            {
                Thread.Sleep(5);
                if (UOProxy.Logger.MsgLog.Count >= 1)
                {
                    lock (UOProxy.Logger.MsgLog)
                    {
                        foreach (string s in UOProxy.Logger.MsgLog)
                        {
                            Console.WriteLine(s);
                        }
                        UOProxy.Logger.MsgLog.Clear();
                    }

                }
            }
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
            Console.WriteLine(e.ToString());
        }

        static void proxy_EventUpdatePlayer(UOProxy.Packets.FromServer._0x77UpdatePlayer e)
        {
            Console.WriteLine(e.ToString());
        }


    }
}
