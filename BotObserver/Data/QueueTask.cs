using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotObserver.Data
{
    public class QueueTask
    {
        public int No { get; set; }
        public string ID { get; set; }
        public string TaskFile { get; set; }
        public string RequestTime { get; set; }
        public string Requester { get; set; }


        public string RegNumberCount { get; set; }

        public string Status { get; set; }           // Ready, Start, Fail, Success


        public string FilePath { get; set; }



        public string EST { get; set; }

        public string Func_1 { get; set; }


        public string Bot_ID { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public QueueTask()
        {
            No = 0;
            ID = string.Empty;
            TaskFile = string.Empty;
            RequestTime = string.Empty;
            Requester = string.Empty;
            RegNumberCount = string.Empty;
            Status = string.Empty;
            FilePath = string.Empty;
            EST = string.Empty;
            Func_1 = string.Empty;
            Bot_ID = string.Empty;
        }
    }
}
