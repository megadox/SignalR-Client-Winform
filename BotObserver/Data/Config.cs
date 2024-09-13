using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotObserver.Data
{
    public class Config
    {
        //public const string PLAYER = "FpPlayer.exe";
        public const string PLAYER = "BA-Assist.exe";
        const string DEFAULT_LOCAL_ROOT = "BA-Assist";

        const string TABLE = Config.Groups.Setting;

        /// <summary>
        /// 디폴트 Root 폴더
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultRoot(string subfolder = "")
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), DEFAULT_LOCAL_ROOT, subfolder);
        }

        public class Groups
        {
            /// <summary>환경설정 그룹</summary>
            public const string Setting = "config";

            /// <summary>단축키 그룹</summary>
            public const string Shortcut = "shortcuts";

            /// <summary>Fp 파일과 Task명 매핑 그룹</summary>
            public const string Task = "tasks";
        }

    }
}
