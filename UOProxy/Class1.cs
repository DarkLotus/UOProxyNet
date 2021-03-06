﻿/*
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
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using UOProxy.Packets.FromServer;
using UOProxy.Packets;

namespace UOProxy
{
    public partial class UOProxy
    {
        
        public TcpListener tcpListener;
        Thread ClientComThread;
        public static bool ProxyMode = false;
        //When using as a proxy, Call this method, Starts listening for local client connection, once client connects, sends connect to Server
        public bool StartListeningForClient(int port)
        {

            try
            {
                SetupHandlers();
                Helpers.Cliloc Clilocdata = new Helpers.Cliloc();
                Helpers.Cliloc.LoadStringList("enu");
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(this.AcceptClientConnection), tcpListener);
                //var client = tcpListener.AcceptTcpClient();
                //AcceptClientConnection(client);
                Logger.Log("Listening for incoming Client connections on: " + IPAddress.Loopback + ":" + port);
                ProxyMode = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void AcceptClientConnection(IAsyncResult ar)
        {
            TcpListener client = (TcpListener)ar.AsyncState;
            Logger.Log("Accepted Client Connection, Starting message loop and connecting to server");
            ClientComThread = new Thread(new ParameterizedThreadStart(HandleClientCom));
            TcpClient UOClient = client.EndAcceptTcpClient(ar);
            ClientComThread.Start(UOClient);
            client.BeginAcceptTcpClient(new AsyncCallback(this.AcceptClientConnection), tcpListener);
            
        }

        public TcpClient Server;
        public void SendToServer(Packet p)
        {
           
            this.Server.GetStream().Write(p.Data.ToArray(), 0, (int)p.Data.Length);
            this.Server.GetStream().Flush();
            Logger.Log(BitConverter.ToString(p.PacketData, 0, p.PacketData.Length) + "len: " + p.PacketData.Length + " Sent To Server");
        }

        public void SendToServer(byte[] p)
        {
            this.Server.GetStream().Write(p, 0, (int)p.Length);
            this.Server.GetStream().Flush();
        }
        public void SendToServer(byte[] p,int length)
        {
            this.Server.GetStream().Write(p, 0, length);
            this.Server.GetStream().Flush();
        }
       
        private int GetClientPacketSize(byte[] data, int bytesRead)
        {
            // need to handle byte packet i guess??
            if (ClientSizes(data[0]) != 0)
            {
                return ClientSizes(data[0]);
            }
            else if(bytesRead > 3)
            {  // Known packet with unknown size or size of 0 try and read size from data[]
                var tempstream = new UOStream(data);
                tempstream.ReadBit();
                return tempstream.ReadShort();
            }
            
             return 0;
        
        }
        private int GetServerPacketSize(byte[] data, int bytesRead)
        {
            int size = 0;
            switch (data[0])
            {
                case 0x8C:
                    size = 11;
                    break;
                default:
                    break;
            }
            if (size > 0)
                return size;
            else
            {
                // no client handler, what to do?
                var tempstream = new UOStream(data);
                tempstream.ReadBit();
                return tempstream.ReadShort();
            }
         }
        private void HandleClientCom(object Client)
        {
            TcpClient client = (TcpClient)Client;
            NetworkStream ClientStream = client.GetStream();
            Server = ConnectToServer("69.162.65.42", 2593, client);
            byte[] tempdata = new byte[4096];
            List<byte> IncomingQueue = new List<byte>();
            while (client.Connected)
            {
                Thread.Sleep(10);
                //Message pump for comm from/to client
                
                if (client.Available <= 0)
                    continue;
                int bytesRead = ClientStream.Read(tempdata, 0, client.Available);
                byte[] data = new byte[bytesRead];
                Array.Copy(tempdata, data, bytesRead);
                SendToServer(data, data.Length);
                if (IncomingQueue.Count > 0)
                {
                    IncomingQueue.AddRange(data);
                    data = IncomingQueue.ToArray();
                    IncomingQueue = new List<byte>();
                    bytesRead = data.Length;
                }
                if (data.Length > 6)
                {
                    if (data[4] == 0x91 && data[0] == data[5])
                    {
                        // Client sends goddamm key on its own before sending it again in 0x91, keeping client/server streams seperate only way to do this?
                        byte[] temp = new byte[data.Length - 4];
                        Array.Copy(data, 4, temp, 0, data.Length - 4);
                        data = temp;
                    }
                }

                    //Logger.Log("From Client: " + BitConverter.ToString(data, 0, bytesRead));
                if (data.Length >= GetClientPacketSize(data, data.Length))
                {
                    HandleClientPacket(data, data.Length);
                }
                else
                { 
                    IncomingQueue.AddRange(data); }              
            }
            if(Server.Connected)
            Server.Close();
            Logger.Log("Client Thread Dying");
        }

      
        byte[] TempdataBuffer = new byte[8192];
        public static bool UseHuffman = false;
        private void HandleServerCom(object cliserv)
        {
            List<byte> IncomingQueue = new List<byte>();
            ClientServer TcpClients = (ClientServer)cliserv;
            HuffmanDecompression Decompressor = new HuffmanDecompression();
            NetworkStream ServerStream = TcpClients.server.GetStream();
            Server = TcpClients.server;
            int bytesRead = 0;
            while (TcpClients.server.Connected)
            {
                //MessagePump from/to Server
                Thread.Sleep(10);
                bytesRead = 0;
                byte[] data = null;
                try
                {
                    if (TcpClients.server.Available > 0)
                    {
                        bytesRead = ServerStream.Read(TempdataBuffer, 0, 8192);
                        data = new byte[bytesRead];
                        Array.Copy(TempdataBuffer, 0, data, 0, bytesRead);
                        if (TcpClients.client != null && UseHuffman && TcpClients.client.Connected)
                            TcpClients.client.GetStream().Write(data, 0, bytesRead);// this is here so we send uncompressed for now, No compress method
                    }

                }
                catch { }

                if (IncomingQueue.Count > 0)
                {
                    if(data != null)
                    IncomingQueue.AddRange(data); 
                    data = IncomingQueue.ToArray();
                    IncomingQueue = new List<byte>();
                    bytesRead = data.Length;
                }
                if (data == null)
                    continue;
                if (UseHuffman)
                {
                    
                    byte[] dest = new byte[4096];
                    int destSize = 0;
                    int datalen = data.Length;
                    while (Decompressor.DecompressOnePacket(ref data, bytesRead, ref dest, ref destSize))
                    {                        
                        byte[] destTrimmed = new byte[destSize];
                        Array.Copy(dest, 0, destTrimmed, 0, destSize);
                        //Logger.Log("From Server DeHuffed: " + BitConverter.ToString(destTrimmed, 0, destSize));
                        HandlePacketFromServer(destTrimmed, TcpClients.client);
                        bytesRead = data.Length;
                    }
                    if (data.Length > 0)
                    {
                        //Logger.Log("NoFull Packets adding " + data.Count() + " to queue");
                        IncomingQueue.AddRange(data);
                    }
                }
                else
                {

                    if (data.Length == GetServerPacketSize(data, bytesRead))
                    {
                        HandlePacketFromServer(data, TcpClients.client);
                        if (data[0] == 0x8c)
                        {
                            TcpClients.server.Close();
                        }
                    }
                    else if (data.Length > GetServerPacketSize(data, bytesRead) && GetServerPacketSize(data, bytesRead) != 0)
                    {
                        byte[] temp = new byte[GetServerPacketSize(data, bytesRead)];
                        Array.Copy(data, temp, GetServerPacketSize(data, bytesRead));
                        HandlePacketFromServer(temp, TcpClients.client);
                        if (temp[0] == 0x8c)
                        {
                            TcpClients.server.Close();
                        }
                        temp = new byte[data.Length - GetServerPacketSize(data, bytesRead)];
                        Array.Copy(data, GetServerPacketSize(data, bytesRead), temp, 0, temp.Length);
                        IncomingQueue.AddRange(temp);
                    }
                    else
                    { IncomingQueue.AddRange(data); }
                    
                    //Logger.Log("From Server NoHuff: " + BitConverter.ToString(data, 0, bytesRead));

                    if (TcpClients.client != null)
                    TcpClients.client.GetStream().Write(data, 0, bytesRead);
                    
                }

            }
            Logger.Log("Server Thread Dying Gracefully");
        }

       

        // Connects to a server and returns Server TcpClient if sucessful, Called when using in Nonproxy mode.
        public TcpClient ConnectToServer(string ip, int port)
        {
            try
            {
                SetupHandlers();
                Helpers.Cliloc Clilocdata = new Helpers.Cliloc();
                Helpers.Cliloc.LoadStringList("enu");
                var Server = new TcpClient();
                IPAddress IP;
                if (IPAddress.TryParse(ip, out IP))
                    Server.Connect(IP, port);
                else
                Server.Connect(ip, port);
                
                Thread ServerComThread = new Thread(new ParameterizedThreadStart(HandleServerCom));
                ServerComThread.Start(new ClientServer(null, Server));
                this.Server = Server;
                return Server;
            }
            catch
            {
                return null;
            }
            
        }
        private TcpClient ConnectToServer(string ip, int port,TcpClient Client)
        {
            TcpClient Server = new TcpClient();
            Server.Connect(IPAddress.Parse(ip), port);
            Thread ServerComThread = new Thread(new ParameterizedThreadStart(HandleServerCom));
            ServerComThread.Start(new ClientServer(Client, Server));
            return Server;

        }

     

        private class ClientServer
        {
            public TcpClient client;
        public TcpClient server;
        public ClientServer(TcpClient Client, TcpClient Server)
        {
            client = Client;
            server = Server;
        }
        }

     
    }
}