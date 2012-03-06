using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy
{
    public static class Logger
    {
        public static List<string> MsgLog = new List<string>();
        public static List<DateTime> MsgTimeStamp = new List<DateTime>();
        public static void Log(string msg)
        {
            lock (MsgLog)
            {
                MsgLog.Add(msg);
                MsgTimeStamp.Add(DateTime.Now);
            }
            
        }
        public static void SaveLog()
        {
            System.IO.FileStream fs = new System.IO.FileStream("log.txt", System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            lock (MsgLog)
            {
                for(int i=0;i < MsgLog.Count;i++)
                {
                    sw.WriteLine(MsgTimeStamp[i].ToShortTimeString() + " : " + MsgLog[i]);
                }
                sw.Close();
                fs.Close();
                MsgLog.Clear();
                MsgTimeStamp.Clear();
            }
        }
    }
}
