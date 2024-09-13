using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotObserver.Data
{
    public class QueueTaskInfo
    {
        public string Queue_ID { get; set; }          // 접수 ID
        public string TaskFile { get; set; }         // BA-Player에서 실행할 TaskFile ( salesReport.fpx )
        public string Filepath { get; set; }        // TaskFile에서 읽어야 할 NAS의 Filepath - 사업자번호등의 정보를 가지고 있음.
    }
}
