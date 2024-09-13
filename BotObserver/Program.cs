using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotObserver
{
    static class Program
    {
        static System.Threading.Mutex _SingletonMutex;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (IsApplicationRun())
                {
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainStarter());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// 중복 실행 방지
        /// </summary>
        /// <returns>true = 중복 실행, false = 신규 실행</returns>
        static bool IsApplicationRun()
        {
            GuidAttribute attribute = (GuidAttribute)CurrentAssembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            string guid = attribute.Value;


            bool createdNew;
            _SingletonMutex = new System.Threading.Mutex(true, guid, out createdNew);

            return !createdNew;
        }

        static Assembly currentAssemblyCore;
        public static Assembly CurrentAssembly
        {
            get
            {
                if (currentAssemblyCore == null)
                    currentAssemblyCore = Assembly.GetExecutingAssembly();
                return currentAssemblyCore;
            }
        }
    }
}
