using DevExpress.Utils.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace BotObserver.Data
{
    public class RDADashboard
    {

        public string Bot_ID { get; set; }
        public string Bot_Name { get; set; }
        public string Descript { get; set; }
        public bool Enabled { get; set; }
        public string BASE_URL { get; set; }
        public string SubPath { get; set; }
        public bool UseRemoteTask { get; set; }
        public string Player { get; set; }
        public string Taskpath { get; set; }
    }

    public class RDADashboard_Service
    {
        const string TABLE = "dashboard_bot";
        public static string _base_url;
        public static string _subpath;
        public static string DashBoardBot_Id = string.Empty;
        public static string DBError = string.Empty;


        public static RDADashboard Get()
        {
            RDADashboard dashboard = new RDADashboard();
            try
            {
                SQLite sqlite = new SQLite();
                sqlite = Open();
                string sql = string.Format(@"select bot_id, bot_name, descript, base_url,subpath, 
                enabled, useremotetask, player, taskpath from {0}",
                    TABLE);

                if (sqlite.ExecuteReader(sql.ToString()))
                {
                    SQLiteDataReader reader = sqlite.GetReader();
                    while (reader.Read())
                    {
                        RDADashboard board = new RDADashboard();

                        board.Bot_ID = reader["bot_id"].ToString();
                        board.Bot_Name = reader["bot_name"].ToString();
                        board.Descript = reader["descript"].ToString();
                        board.Enabled = reader["enabled"].ToString() == "1" ? true : false;
                        board.BASE_URL = reader["base_url"].ToString();
                        board.SubPath = reader["subpath"].ToString();
                        board.UseRemoteTask = reader["useremotetask"].ToString() == "1" ? true : false;
                        board.Player = reader["player"].ToString();
                        board.Taskpath = reader["taskpath"].ToString();
                        dashboard = board;

                        DataHelper.BASE_URL = board.BASE_URL;
                        DataHelper.SubPath = board.SubPath;

                        break;
                    }
                }

                sqlite.Close();
                return dashboard;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                DBError = ex.ToString();
                return dashboard;
            }
        }

        private static SQLite Open()
        {
            try
            {
                string dbpath = GetDBPath();
                ClsStruct.dbPath_ = dbpath;

                SQLite sqllite = new SQLite(dbpath);

                sqllite.CreateTable(TABLE, new string[]
                {
                    "'bot_id' VARCHAR(100)",
                    "'bot_name' VARCHAR(200)",
                    "'descript' VARCHAR(300)",
                    "'base_url' VARCHAR(300)",
                    "'subpath' VARCHAR(300)",
                    "'enabled' INTEGER",
                    "'useremotetask' INTEGER",
                    "'player' VARCHAR(300)",
                    "'taskpath' VARCHAR(300)",
                });

                return sqllite;
            }
            catch (Exception ex)
            {
                ClsStruct.Errors = ex.ToString();
                throw ex;
            }
        }

        public static string GetDBPath()
        {
            return Path.Combine(
                Config.GetDefaultRoot(),
                Properties.Settings.Default.DBPath,
                Properties.Settings.Default.DBTasks
            );
        }
    }
}
