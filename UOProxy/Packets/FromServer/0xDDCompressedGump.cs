using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xDDCompressedGump : Packet
    {
        short length;
        public int PlayerID;
        public int GumpID, X, Y;

        int compressedGumpLength, decompressedGumpLength;
        byte[] GumpData;

        int NumberTextLines,CompressedTextLen,DecompressedTextLen;
        byte[] GumpTextData;

        List<string> Text = new List<string>();
        public _0xDDCompressedGump(UOStream Data)
            : base(Data)
        {
            try
            {
                length = Data.ReadShort();
                PlayerID = Data.ReadInt();
                GumpID = Data.ReadInt();
                X = Data.ReadInt();
                Y = Data.ReadInt();
                compressedGumpLength = Data.ReadInt() - 4;
                decompressedGumpLength = Data.ReadInt();
                if (compressedGumpLength > 1)
                {
                    GumpData = new byte[compressedGumpLength];
                    Data.Read(GumpData, 0, compressedGumpLength);
                }


                NumberTextLines = Data.ReadInt();
                CompressedTextLen = Data.ReadInt() - 4;
                DecompressedTextLen = Data.ReadInt();
                if (CompressedTextLen > 0)
                {
                    GumpTextData = new byte[CompressedTextLen];
                    Data.Read(GumpTextData, 0, CompressedTextLen);
                }


                //byte[] UncompressedGumpData = new byte[decompressedGumpLength];
                //OpenUO.Core.IO.ZlibCompression.Unpack(UncompressedGumpData,ref decompressedGumpLength,GumpData,compressedGumpLength);

                /*System.IO.MemoryStream outstream = new System.IO.MemoryStream();
                zlib.ZOutputStream outZstream = new zlib.ZOutputStream(outstream);
                System.IO.MemoryStream input = new System.IO.MemoryStream(GumpData);
                CopyStream(input, outZstream);
                outZstream.finish();
                outZstream.end();
                byte[] UncompressedGumpData = new byte[outZstream.TotalOut];
                outZstream.Position = 0;
                outZstream.Read(UncompressedGumpData, 0, (int)outZstream.TotalOut);
                */
                //byte[] UncompressedGumpText = new byte[DecompressedTextLen];
                //OpenUO.Core.IO.ZlibCompression.Unpack(UncompressedGumpText,ref DecompressedTextLen,GumpTextData,DecompressedTextLen);
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString());
            }
            
        }

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
    }
}
