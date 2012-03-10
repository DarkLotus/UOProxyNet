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
using System.Text;
using System.IO;

namespace UOProxy
{
    public class UOStream : MemoryStream
    {
        byte[] _buffer;
        int _index;
        int _length;
        public UOStream(byte[] Data) : base(Data)
        {
            this.Position = 0;
            this._buffer = Data;
            this._index = 0;
            this._length = Data.Length;

        }
        public UOStream() : base()
        {

        }
        public void WriteInt(int value)
        {
            byte[] _buffer = new byte[4];
            _buffer[0] = (byte)(value >> 24);
            _buffer[1] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 8);
            _buffer[3] = (byte)value;
            this.Write(_buffer, 0, 4);
        }

        public void WriteUInt(uint value)
        {
            byte[] _buffer = new byte[4];
            _buffer[0] = (byte)(value >> 24);
            _buffer[1] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 8);
            _buffer[3] = (byte)value;
            this.Write(_buffer, 0, 4);
        }
        public void WriteBit(byte Value)
        {
            this.Write(new byte[] { Value }, 0, 1);
        }
        public void WriteShort(short value)
        {
            byte[] _buffer = new byte[2];
            _buffer[0] = (byte)(value >> 8);
            _buffer[1] = (byte)value;

            this.Write(_buffer, 0, 2);
        }
        public void WriteString(string Value,int RequiredLength)
        {
            //RequiredLength 0 if you dont need padding.
            System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
            if (RequiredLength == 0)
            {
                byte[] Data = encoding.GetBytes(Value);
                this.Write(Data, 0, Data.Length);
            }
            else
            {
                byte[] Data = new byte[RequiredLength];
                for (int i = 0; i < Data.Length;i++)
                {
                    Data[i] = 0x00;
                }
                if(Data.Length < RequiredLength)
                     encoding.GetBytes(Value).CopyTo(Data,0);
                
            }
        }
        public int ReadInt()
        {
            byte[] results = new byte[4]; this.Read(results, 0, 4);
            return (int)((results[0] << 24 | results[1] << 16) | (results[2] << 8) | results[3]);
        }
        public uint ReadUInt()
        {
            byte[] results = new byte[4]; this.Read(results, 0, 4);
            return (uint)((results[0] << 24 | results[1] << 16) | (results[2] << 8) | results[3]);
        }
        public byte ReadBit()
        {
            byte[] results = new byte[1]; this.Read(results, 0, 1);
            return results[0];
        }

        public byte PeekBit()
        {
            byte[] results = new byte[1]; this.Read(results, 0, 1);
            this.Position--;
            return results[0];
        }


        public byte[] ReadBytes(int NumToRead)
        {
            byte[] results = new byte[NumToRead]; this.Read(results, 0, NumToRead);
            return results;
        }

        public short ReadShort()
        {
            byte[] results = new byte[2]; this.Read(results, 0, 2);
            return (short)(results[0] << 8 | results[1]);
        }

        public string ReadString(int bytesToRead)
        {
            byte[] mystring = new byte[bytesToRead];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytesToRead; i++)
            {
                //mystring[i] = this.ReadBit();
                byte temp = this.ReadBit();
                //byte temp2 = this.ReadBit();
                //char mychar = (char)((temp << 8) | (temp2));
                if(temp > 0)
                sb.Append((char)temp);
            }
            return sb.ToString();
            return UTF8Encoding.UTF8.GetString(mystring).Replace("\0", "");
        }
        public string ReadNullTermString()
        {
            byte current;
            byte previous = 1;
            List<byte> ms = new List<byte>();
            while (true)
            {
                current = this.ReadBit();
                if (current == 0x00 && previous == 0x00) // Reads untill null terminated
                    break;
                ms.Add((byte)current);
                previous = (byte)(current + 0);

            }
            return UTF8Encoding.UTF8.GetString(ms.ToArray()).Replace("\0","");
            
        }


       
    }
}
