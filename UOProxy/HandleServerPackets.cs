using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UOProxy.Packets.FromServer;
using UOProxy.Packets;
using System.Net.Sockets;
namespace UOProxy
{
    public partial class UOProxy
    {
        public event UpdatePlayerEventHandler EventUpdatePlayer;
        public delegate void UpdatePlayerEventHandler(_0x77UpdatePlayer e);

        public event ObjectInfoEventHandler EventObjectInfo;
        public delegate void ObjectInfoEventHandler(_0x1AObjectInfo e);

        public event ConnectToGameServerEventHandler EventConnectToGameServer;
        public delegate void ConnectToGameServerEventHandler(_0x8CConnectToGameServer e);

        public event StatusBarInfoEventHandler EventStatusBarInfo;
        public delegate void StatusBarInfoEventHandler(_0x11StatusBarInfo e);

        public event SendSpeechEventHandler EventSendSpeech;
        public delegate void SendSpeechEventHandler(_0x1CSendSpeech e);

        public event MobAttributeEventHandler EventMobAttribute;
        public delegate void MobAttributeEventHandler(_0x2DMobAttributes e);
        //public event AAAEventHandler EventAAA;
        //public delegate void AAAAEventHandler(_0x11StatusBarInfo e);

        private void HandlePacketFromServer(byte[] data, TcpClient client)
        {
            //uncompressed packets may arrive with more than one in data;
            UOStream Data = new UOStream(data);
            while (Data.Position < Data.Length)
            {
                Packet p = new Packet();
                switch (data[Data.Position])
                {
                    case OpCode.SMSG_StatusBarInfo:
                        p = new _0x11StatusBarInfo(Data);
                        if (EventStatusBarInfo != null)
                            EventStatusBarInfo((_0x11StatusBarInfo)p);
                        break;

                    case OpCode.SMSG_ObjectInfo:
                        p = new _0x1AObjectInfo(Data);
                        if (EventObjectInfo != null)
                            EventObjectInfo((_0x1AObjectInfo)p);
                        break;

                    case OpCode.SMSG_SendSpeach:
                        p = new _0x1CSendSpeech(Data);
                        if (EventSendSpeech != null)
                            EventSendSpeech((_0x1CSendSpeech)p);
                        break;
                    case OpCode.SMSG_MobAttribute:
                        p = new _0x2DMobAttributes(Data);
                        if (EventMobAttribute != null)
                            EventMobAttribute((_0x2DMobAttributes)p);
                        break;

                    case OpCode.SMSG_UpdatePlayer:
                        p = new _0x77UpdatePlayer(Data);
                        if (EventUpdatePlayer != null)
                            EventUpdatePlayer((_0x77UpdatePlayer)p);
                        break;
                    case OpCode.SMSG_ConnectToGameServer:
                        p = new _0x8CConnectToGameServer(Data);
                        if (EventConnectToGameServer != null)
                            EventConnectToGameServer((_0x8CConnectToGameServer)p);
                        UOProxy.UseHuffman = true;
                        break;
                    default:
                        Data.Position = Data.Length;
                        break;

                }
                /*if (p != null)
                {
                    if (client != null) // Send Packet onto client fuck compress it :|
                        client.GetStream().Write(p.PacketData, 0, p.PacketData.Length);
                }
                else
                {
                    if (client != null) // Send Packet onto client fuck compress it :|
                        client.GetStream().Write(data, 0, data.Length);
                }*/
               


            }

        }
    }

    public static class OpCode
    {
        // Packets sent by client and server
        public const int MSG_CharMoveACK = 0x22;
        public const int MSG_PingMessage = 0x73;

        public const int MSG_TargetCursorCommands = 0x6C;
        public const int MSG_SendSkills = 0x3A;
        public const int MSG_SecureTrading = 0x6F;
        public const int MSG_AllNames = 0x98;
        public const int SMSG_SendSpeach = 0x1C;
        public const int MSG_ClientVersion = 0xBD;
        public const int MSG_RequestWarMode = 0x72;
        // Packets sent from the server
        public const int SMSG_GameServlist = 0xA8;
        public const int SMSG_DrawObject = 0x78;
        public const int SMSG_SetWeather = 0x65;
        public const int SMSG_WornItem = 0x2E;
        public const int SMSG_Deleteobject = 0x1D;
        public const int SMSG_UpdatePlayer = 0x77;
        public const int SMSG_ClientFeatures = 0xB9;
        public const int SMSG_GeneralInformation = 0xBF;
        public const int SMSG_CharLocAndBody = 0x1B;
        public const int SMSG_OverallLightLevel = 0x4F;
        public const int SMSG_ObjectInfo = 0x1A;
        public const int SMSG_StatusBarInfo = 0x11;
        public const int SMSG_DrawGamePlayer = 0x20;
        public const int SMSG_Damage = 0x0B;
        public const int SMSG_CharMoveRejection = 0x21;
        public const int SMSG_DraggingOfItem = 0x23;
        public const int SMSG_DrawContainer = 0x24;
        public const int SMSG_AddItemToContainer = 0x25;
        public const int SMSG_KickPlayer = 0x26;
        public const int SMSG_RejectMoveItemRequest = 0x27;
        public const int SMSG_DropItemApproved = 0x29;
        public const int SMSG_Blood = 0x2A;
        public const int SMSG_MobAttribute = 0x2D;
        public const int SMSG_FightOccuring = 0x2F;
        public const int SMSG_AttackOK = 0x30;
        public const int SMSG_AddmultipleItemsInContainer = 0x3C;
        public const int SMSG_PersonalLightLevel = 0x4E;
        public const int SMSG_IdleWarning = 0x53;
        public const int SMSG_PlaySoundEffect = 0x54;
        public const int SMSG_CharacterAnimation = 0x6E;
        public const int SMSG_GraphicalEffect = 0x70;
        public const int SMSG_OpenBuyWindow = 0x74;
        public const int SMSG_OpenDialogBox = 0x7C;
        public const int SMSG_OpenPaperdoll = 0x88;
        public const int CMSG_MovePlayer = 0x97;
        public const int SMSG_SellList = 0x9E;
        public const int SMSG_UpdateCurrentHealth = 0xA1;
        public const int SMSG_UpdateCurrentMana = 0xA2;
        public const int SMSG_UpdateCurrentStamina = 0xA3;
        public const int SMSG_AllowRefuseAttack = 0xAA;
        public const int SMSG_GumpTextEntryDialog = 0xAB;
        public const int SMSG_SendGumpMenuDialog = 0xB0;
        public const int SMSG_CliocMessage = 0xC1;
        public const int SMSG_LoginDenied = 0x82;
        public const int SMSG_ConnectToGameServer = 0x8C;
        public const int SMSG_CharList = 0xA9;
        public const int SMSG_GameServerList = 0xA8;
        public const int SMSG_ServerChat = 0xAE;
        public const int SMSG_Time = 0x5B;
        public const int SMSG_SEintroducedRevision = 0xDC;
        public const int SMSG_Seasonalinformation = 0xBC;


        // Packets sent only via client
        public const int CMSG_GetPlayerStatus = 0x34;
        public const int CMSG_DropItem = 0x08;
        public const int CMSG_Loginreq = 0x80;
        public const int CMSG_Pathfind = 0x38; // runuo doesnt support this
        public const int CMSG_SingleClick = 0x09;
        public const int CMSG_DoubleClick = 0x06;
        public const int CMSG_PickUpItem = 0x07;
        public const int CMSG_DissconnectNotification = 0x01;
        public const int CMSG_MoveRequest = 0x02;
        public const int CMSG_TalkRequest = 0x03;
        public const int CMSG_RequestAttack = 0x05;
        public const int CMSG_RequestSkilluse = 0x12;
        public const int CMSG_DropWearitem = 0x13;
        public const int CMSG_BuyItems = 0x3B;
        public const int CMSG_RequestWarMode = 0x72;
        public const int CMSG_ResponseToDialogBox = 0x7D;
        public const int CMSG_SellListReply = 0x9F;
        public const int CMSG_ClientSPy = 0xA4;
        public const int CMSG_GumpTextEntryDialogReply = 0xAC;
        public const int CMSG_GumpMenuSelection = 0xAD;
        public const int CMSG_SpyOnClient = 0xD9;
        public const int CMSG_LoginRequest = 0x80;
        public const int CMSG_SelectServer = 0xA0;
        public const int CMSG_GameServerLogin = 0x91;
        public const int CMSG_LoginChar = 0x5D;
        public const int CMSG_ClientVersion = 0xBD;
    }
}
