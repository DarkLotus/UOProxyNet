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

namespace UOProxy.Helpers
{
    public class Item
    {
        public readonly int Serial;
        public readonly short GraphicID;
        public readonly short Amount, X, Y;
        public readonly byte GridIndex;
        public readonly int ContainerSerial;
        public readonly short Hue;
        public Item(int serial, short graphicid, short amount, short X, short Y, byte gridIndex, int ContainerSerial, short Hue)
        {
            this.Serial = serial;
            this.GraphicID = graphicid;
            this.Amount = amount;
            this.X = X;
            this.Y = Y;
            this.GridIndex = gridIndex;
            this.ContainerSerial = ContainerSerial;
            this.Hue = Hue;
        }
        public Item(Packets.FromServer._0x25AddItemToContainer packet)
        {
            this.Serial = packet.Serial;
            this.GraphicID = packet.GraphicID;
            this.Amount = packet.Amount;
            this.X = packet.X;
            this.Y = packet.Y;
            this.GridIndex = packet.Index;
            this.ContainerSerial = packet.ContainerSerial;
            this.Hue = packet.Hue;
        }
    }
}
