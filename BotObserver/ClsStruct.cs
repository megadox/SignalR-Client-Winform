using DevExpress.Utils.Layout;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotObserver
{
    public class ClsStruct
    {
        public static bool SETTING_VIEW;
        public static string BASE_URL;
        public static string SubPath;
        public static string Bot_ID;
        public static string Bot_Name;
        public static bool Application_Exit;
        public static HubConnection hubConnection_;
    }
}
