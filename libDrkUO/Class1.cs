using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libDrkUO
{
    public partial class libDrkUO
    {
        public Dictionary<int, Item> Objects = new Dictionary<int, Item>();
        public Dictionary<int, Mobile> Mobiles = new Dictionary<int, Mobile>();
        private UOProxy.UOProxy proxy;
        private TcpClient tcpserver;
        private bool _loggedIn = false;
                private int Keyy = 0;
                byte key;
        string server; int port;
        public libDrkUO(string server,int port,string username,string password,byte charSlot)
        {
            key = (byte)new Random().Next(255);
            UOProxy.Packets.Packet packet;
            proxy = new UOProxy.UOProxy();
            proxy._0x0BDamage += proxy__0x0BDamage;
            proxy._0x11StatusBarInfo += proxy__0x11StatusBarInfo;
            proxy._0x16StatusBarUpdate += proxy__0x16StatusBarUpdate;
            proxy._0x1BCharLocaleBody += proxy__0x1BCharLocaleBody;
            proxy._0x1CSendSpeech += proxy__0x1CSendSpeech;
            proxy._0x1DDeleteObject += proxy__0x1DDeleteObject;
            proxy._0x20DrawGamePlayer += proxy__0x20DrawGamePlayer;
            proxy._0x21CharMoveRejection += proxy__0x21CharMoveRejection;
            proxy._0x22MoveAck += proxy__0x22MoveAck;
            proxy._0x24DrawContainer += proxy__0x24DrawContainer;
            proxy._0x25AddItemToContainer += proxy__0x25AddItemToContainer;
            proxy._0x2DMobAttributes += proxy__0x2DMobAttributes;
            proxy._0x2EWornItem += proxy__0x2EWornItem;
            proxy._0x3CAddMultipleItemsToContainer += proxy__0x3CAddMultipleItemsToContainer;
            proxy._0x55LoginComplete += proxy__0x55LoginComplete;
            proxy._0x6CTargetCursorCommands += proxy__0x6CTargetCursorCommands;
            proxy._0x73Ping += proxy__0x73Ping;
            proxy._0x77UpdatePlayer += proxy__0x77UpdatePlayer;
            proxy._0x78DrawObject += proxy__0x78DrawObject;
            proxy._0x8CConnectToGameServer += proxy__0x8CConnectToGameServer;
            proxy._0xA8GameServerList += proxy__0xA8GameServerList;
            proxy._0xA9CharStartingLocation += proxy__0xA9CharStartingLocation;
            proxy._0xBDClientVersion += proxy__0xBDClientVersion;
            proxy._0xC1ClilocMessage += proxy__0xC1ClilocMessage;
            proxy._0xD6MegaCliloc += proxy__0xD6MegaCliloc;
            proxy._0xDDCompressedGump += proxy__0xDDCompressedGump;
            proxy._0xF3ObjectInfo += proxy__0xF3ObjectInfo;
            tcpserver = proxy.ConnectToServer(server, port);
            if (tcpserver == null)
            {
                throw new Exception("Unable to Connect");
            }
            this.server = server;
            this.port = port;
            packet = new UOProxy.Packets.FromClient._0xEFClientLoginSeed(IPAddress.Parse("192.168.2.3"), 7, 0, 23, 1);
            proxy.SendToServer(packet);
            packet = new UOProxy.Packets.FromClient._0x80LoginRequest(username, password, key);
            proxy.SendToServer(packet);
            //packet = new UOProxy.Packets.FromBoth._0x73Ping(0);
            //proxy.SendToServer(packet);
            //Thread mythread = new Thread(new ThreadStart(clientloop));
            //mythread.Start();

            

        }

      
        private void clientloop()
        {
            while (true)
            {
               
                Thread.Sleep(5);
            }
        }

     

        public void SaveLog()
        { UOProxy.Logger.SaveLog();
        proxy = null;
        tcpserver.Close();
        }
       
    }
}
