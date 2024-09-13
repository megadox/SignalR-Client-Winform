using DevExpress.XtraPrinting.Native.WebClientUIControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BotObserver.Data
{
    public class DataHelper
    {
        public static string BASE_URL = string.Empty;
        public static string SubPath = string.Empty;
        public static string TypeName = string.Empty;
        public static string DashboardURL = string.Empty;
        public static string TestURL = "ping";

        public static void SetURL()
        {
            if (BASE_URL.Length == 0)
            {
                RDADashboard rdashboard = RDADashboard_Service.Get();
                if (rdashboard != null)
                {
                    BASE_URL = rdashboard.BASE_URL;
                    SubPath = rdashboard.SubPath;
                }
            }
        }

        /// <summary>
        /// Dashboard 접속 테스트 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool REST_Test()
        {
            //return true;

            if (DashboardURL.Trim().Length == 0)
            {
                SetURL();
                DashboardURL = $"{BASE_URL}{SubPath}{TestURL}";
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var result = client.GetAsync(DashboardURL).Result;

                    if (result.IsSuccessStatusCode)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception)
            {
                return false;
            }

        }


        public static async Task<List<T>> Read<T>() where T : new()
        {
            SetURL();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URL);
                    string uri = $"{SubPath}{TypeName.ToLower()}";
                    var result = client.GetAsync(uri).Result;

                    var jsonResult = result.Content.ReadAsStringAsync();

                    if (result.IsSuccessStatusCode)
                    {
                        var objects = JsonConvert.DeserializeObject<List<T>>(jsonResult.Result);
                        List<T> list = new List<T>();
                        if (objects != null)
                        {
                            foreach (var o in objects)
                            {
                                list.Add(o);
                            }
                        }

                        return list;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                string ext = ex.ToString();
                return null;
            }
        }
    }
}
