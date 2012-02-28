using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

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
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                var client = tcpListener.AcceptTcpClient();
                AcceptClientConnection(client);
                Logger.Log("Listening for incoming Client connections on: " + IPAddress.Loopback + ":" + port);
                ProxyMode = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void AcceptClientConnection(TcpClient UOClient)
        {

            Logger.Log("Accepted Client Connection, Starting message loop and connecting to server");
            ClientComThread = new Thread(new ParameterizedThreadStart(HandleClientCom));
            ClientComThread.Start(UOClient);
            
        }


        private void HandleClientCom(object Client)
        {
            TcpClient client = (TcpClient)Client;
            NetworkStream ClientStream = client.GetStream();
            TcpClient Server = ConnectToServer("69.162.65.42", 2593, client);
            while (client.Connected)
            {
                //Message pump for comm from/to client
                byte[] data = new byte[4096];
                if (client.Available <= 0)
                    continue;
                int bytesRead = ClientStream.Read(data, 0, client.Available);
                Logger.Log("From Client: " + BitConverter.ToString(data, 0, bytesRead));
                //Todo parse packet stream, ability to filter certain packet.
                Server.GetStream().Write(data, 0, bytesRead);
            }
        }
        private void HandleServerCom(object cliserv)
        {
            List<byte> IncomingQueue = new List<byte>();
            ClientServer TcpClients = (ClientServer)cliserv;
            HuffmanDecompression Decompressor = new HuffmanDecompression();
            NetworkStream ServerStream = TcpClients.server.GetStream();
            bool UseHuffman = false;
            while (TcpClients.server.Connected)
            {
                //MessagePump from/to Server
                byte[] data = new byte[4096];
                if (TcpClients.server.Available <= 0)
                    continue;
                int bytesRead = ServerStream.Read(data, 0, TcpClients.server.Available);
                if (IncomingQueue.Count > 0)
                {
                    IncomingQueue.AddRange(data); 
                    data = IncomingQueue.ToArray();
                    IncomingQueue = new List<byte>();
                }
                if (UseHuffman)
                {
                    byte[] dest = new byte[1024];
                    int destSize = 0;
                    int bytesConsumed = 0;
                    if (Decompressor.DecompressOnePacket(ref data, bytesRead, ref dest, ref destSize, ref bytesConsumed))
                    {
                        if (bytesConsumed < bytesRead)
                        {
                            // Must have been multiple packets in buffer.
                            byte[] unusedData = new byte[bytesRead - bytesConsumed]; // buffer for received data that was not uncompressed
                            Array.Copy(data, bytesConsumed, unusedData, 0, bytesRead - bytesConsumed); // copy unused data to new array
                            IncomingQueue.AddRange(unusedData); // add the unused data to the Queue
                            Logger.Log("From Server DeHuffed: " + BitConverter.ToString(dest, 0, bytesRead));
                            //if(TcpClients.client != null)
                            //TcpClients.client.GetStream().Write(data, 0, bytesConsumed); //pass along the consumed data still compressed, we only send data for packets we have whole data for

                            HandlePacketFromServer(data, TcpClients.client);
                        }
                    }
                    else
                    {
                        IncomingQueue.AddRange(data);
                    }
                }
                else
                {
                    //TODO handle packets
                    HandlePacketFromServer(data, TcpClients.client);
                    Logger.Log("From Server NoHuff: " + BitConverter.ToString(data, 0, bytesRead));
                    //if (TcpClients.client != null)
                    //TcpClients.client.GetStream().Write(data, 0, bytesRead);
                    
                }

            }
        }

       

        // Connects to a server and returns Server TcpClient if sucessful, Called when using in Nonproxy mode.
        public TcpClient ConnectToServer(string ip, int port)
        {
            try
            {
                TcpClient Server = new TcpClient();
                Server.Connect(IPAddress.Parse(ip), port);
                Thread ServerComThread = new Thread(new ParameterizedThreadStart(HandleServerCom));
                ServerComThread.Start(new ClientServer(null, Server));
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
/*
 Proxy function passes thru all messages from/to real client
 * 
 
 
 
 */