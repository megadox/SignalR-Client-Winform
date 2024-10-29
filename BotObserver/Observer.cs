using BotObserver.Data;
using DevExpress.DataAccess.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;
using Freeper.Beacon;
using System.IO;
using DevExpress.Utils.Drawing.Helpers;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using DevExpress.XtraEditors.SyntaxEditor;
using System.Management;
using System.Reflection;


namespace BotObserver
{
    public partial class Observer : DevExpress.XtraEditors.XtraForm
    {
        
        bool connectionRetry = true;
        bool liveCheck = false;

        int connectionTry = 10;
        int tryCount = 0;
        string queueServer = string.Empty;
        string ServerMessage = string.Empty;
        string QueueID_ = string.Empty;
        string Filepath_ = string.Empty;
        string TaskName_ = string.Empty;
        //bool MessageReceived = false;
        string latestStatus = string.Empty ;
        string latestStatus_Send = string.Empty ;
        int CheckInterval = 5000;   // 5초

        BeaconTray beaconTray;

        public Observer(BeaconTray _beaconTray)
        {
            InitializeComponent();

            this.beaconTray = _beaconTray;

            this.btnConnect.Click += BtnConnect_Click;
            this.btnDisconnect.Click += BtnDisconnect_Click;
            this.Load += Observer_Load;
            this.Shown += Observer_Shown;
            this.FormClosed += Observer_FormClosed;
            this.FormClosing += Observer_FormClosing;

            this.btnDisconnect.Enabled = false;

             SetData();

            if(!CreateHubConnection())
            {
                this.btnConnect.Enabled = false;
                this.btnDisconnect.Enabled = false;
            }

            this.btnTest.Click += BtnTest_Click;

            Version version = Assembly.GetEntryAssembly().GetName().Version;
            lblVersion.Text = version.ToString();
            ckbLog.CheckedChanged += CkbLog_CheckedChanged;
            ckbLog.Checked = ClsStruct.LogWrite;

        }

        private void CkbLog_CheckedChanged(object sender, EventArgs e)
        {
            ClsStruct.LogWrite = ckbLog.Checked;
        }

        private async void BtnTest_Click(object sender, EventArgs e)
        {
            //QueueTask task = new QueueTask();
            //task.ID = "35";
            //task.TaskFile = "회계-부가세신고-1.0.fpx";
            //task.Status = "Start";

            //DataHelper.BASE_URL = "https://localhost:7060/";
            //DataHelper.SubPath = "TRM/";
            //DataHelper.DetailPath = "updateQueueTask";

            await QueueTaskRun("회계-부가세신고-1.0.fpx", "23");
            //await DataHelper.Update_(task);
        }

        private bool CreateHubConnection()
        {
            try
            {
                if (ClsStruct.hubConnection_ == null || ClsStruct.hubConnection_.State == HubConnectionState.Disconnected)
                {

                    string hubUrl = ClsStruct.BASE_URL + "ChatHub";

                    ClsStruct.hubConnection_ = new HubConnectionBuilder()
                    .WithUrl(new Uri(hubUrl))
                    .Build();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void Observer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ClsStruct.Application_Exit)
            {
                ClsStruct.SETTING_VIEW = false;
                e.Cancel = true;
                this.Hide();
            }
        }

        private void Observer_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
        }

        private void Observer_Shown(object sender, EventArgs e)
        {
            int x_  = this.btnConnect.Location.X;
            int y_ = this.btnConnect.Location.Y + this.btnConnect.Height;

            this.listBox1.Location = new System.Drawing.Point(x_, y_ + 10);
            this.listBox1.Height = 200;
            this.listBox1.Width = txtServer.Width;
            ckbLog.Location = new System.Drawing.Point(lblVersion.Location.X, lblVersion.Location.Y + 30);

            ckbLog.Checked = ClsStruct.LogWrite;

            // 폼이 다시 보일 때 이벤트 핸들러가 정상적으로 설정되어 있는지 확인하고, 재설정한다.
            //RegisterHubConnectionEvents();

            // 폼이 다시 보일 때 PlayerLiveCheck 다시 시작
            if(ClsStruct.hubConnection_ !=null)
            {
                if (ClsStruct.hubConnection_.State == HubConnectionState.Connected)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.btnConnect.Enabled = false;
                        this.btnDisconnect.Enabled = true;

                        lblStatus.Text = "Connected";
                        lblStatus.ForeColor = Color.Green;
                    }));
                    StartPlayerLiveCheck();
                }
            }
            
        }

        private void Observer_Load(object sender, EventArgs e)
        {
            this.beaconTray.Show();
            //if(ClsStruct.hubConnection_ !=null && ClsStruct.hubConnection_.State == HubConnectionState.Connected)
            //{
            //    this.Invoke(new Action(() =>
            //    {
            //        string time_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //        listBox1.Items.Insert(0, $"{time_} : Connected.....");
            //        lblStatus.Text = "Connected";
            //        lblStatus.ForeColor = Color.Green;
            //    }));

            //    {
            //        this.btnConnect.Enabled = false;
            //        this.btnDisconnect.Enabled = true;

            //        ClsStruct.hubConnection_.On<string, string>("ReceiveMessage", async (user, message) =>
            //        {
            //            if (MessageReceived) return;

            //            var newMessage = $"{user} : {message}";
            //            string[] arr_msg = message.Split(':');
            //            if (!string.IsNullOrEmpty(message))
            //            {
            //                // 서버에서 메세지를 받으면 상태 체크해서 보내는 로직을 잠시 중지 한다.
            //                MessageReceived = true;
            //                queueServer = user;
            //                ServerMessageParse(message);

            //                Task.Delay(5000);
            //                MessageReceived = false;
            //            }

            //            //await HandleReceivedMessage(user, message);

            //        });

            //        CheckConnectionStateAsync();
            //        PlayerLiveCheck();
            //    }
            //}
        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!CreateHubConnection())
            {
                this.btnConnect.Enabled = false;
                this.btnDisconnect.Enabled = false;

                return;
            }


            this.btnConnect.Enabled = false;
            this.btnDisconnect.Enabled = true;

            
            connectionRetry = true;
            tryCount = 0;
            liveCheck = true;

            // HubConnection 이벤트 핸들러 설정
            RegisterHubConnectionEvents();

            //ClsStruct.hubConnection_.On<string, string>("ReceiveMessage", async (user, message) =>
            //{
            //    if (MessageReceived) return;

            //    var newMessage = $"{user} : {message}";
            //    string[] arr_msg = message.Split(':');
            //    if (!string.IsNullOrEmpty(message))
            //    {
            //        // 서버에서 메세지를 받으면 상태 체크해서 보내는 로직을 잠시 중지 한다.
            //        MessageReceived = true;
            //        queueServer = user;
            //        ServerMessageParse(message);

            //        await Task.Delay(5000);
            //        MessageReceived = false;
            //    }

            //    //await HandleReceivedMessage(user, message);

            //});

            //ClsStruct.hubConnection_.Closed += HubConnection_Closed;

            string bot_id = ClsStruct.Bot_ID;

            try
            {
                await ClsStruct.hubConnection_.StartAsync();
                //this.Invoke(new Action(() =>
                //{
                //    string time_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //    listBox1.Items.Insert(0, $"{time_} : Connected.....");
                //    lblStatus.Text = "Connected";
                //    lblStatus.ForeColor = Color.Green;
                //}));
                UpdateUI("Connected.....", "Connected", Color.Green);
                await ClsStruct.hubConnection_.InvokeAsync("Register", ClsStruct.Bot_ID);
                //listBox1.Items.Add("Connection Started.");

                //_ = CheckConnectionStateAsync();
                //_ = PlayerLiveCheck();
                // 비동기 작업 실행
                //_ = Task.Run(() => PlayerLiveCheck());
                // 비동기 작업 실행
                StartPlayerLiveCheck();
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"XXXXXXX :: {ex.ToString()}");
                //listBox1.Items.Add(ex.Message);

                this.btnConnect.Enabled = true;
                this.btnDisconnect.Enabled = false;

                // 연결 시도 중 예외 발생 시 서버가 오프라인임을 알림
                UpdateUI("Server is offline.", "Disconnected", Color.FromArgb(255, 128, 0));
            }
        }

        private void StartPlayerLiveCheck()
        {
            // 기존의 PlayerLiveCheck가 이미 실행 중인지 확인
            //if (!liveCheck)
            //{
            //    liveCheck = true;
            //    Task.Run(() => PlayerLiveCheck()); // 비동기 작업 실행
            //}


            Task.Run(() => PlayerLiveCheck()); // 비동기 작업 실행
        }

        private async Task HandleReceivedMessage(string user, string  message)
        {
            //if(MessageReceived)
            //{
            //    return;
            //}

            try
            {
                //MessageReceived = true;

                await Task.Delay(CheckInterval);

                string[] arr_msg = message.Split(':');
                if (!string.IsNullOrEmpty(message))
                {
                    // 서버에서 메세지를 받으면 상태 체크해서 보내는 로직을 잠시 중지 한다.
                    //MessageReceived = true;
                    queueServer = user;
                    //ServerMessage = arr_msg[1].Trim();
                    ServerMessageParse(message);
                    //MessageReceived = false;
                }
            }
            finally
            {
                //MessageReceived = false;
            }

        }

        private async Task HubConnection_Closed(Exception arg)
        {
            this.Invoke(new Action(() =>
            {
                string time_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                listBox1.Items.Insert(0, $"{time_} : Disconnected....."); 
                lblStatus.Text = "Disconnect";
                lblStatus.ForeColor = Color.FromArgb(255, 128, 0);
            }));

            await Task.Delay(1000);
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            this.btnConnect.Enabled = true;
            this.btnDisconnect.Enabled = false;
            //MessageReceived = false;    

            tryCount = 0;
            connectionRetry = false;
            liveCheck = false;

            CleraListbox();

            ClsStruct.hubConnection_.DisposeAsync();
            this.Invoke(new Action(() =>
            {
                string time_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                listBox1.Items.Insert(0, $"{time_} : Disconnected.....");
                lblStatus.Text = "Disconnect";
                lblStatus.ForeColor = Color.FromArgb(255, 128, 0);
            }));
        }

        

        private void SetData()
        {
            try
            {
                RDADashboard dashboard = RDADashboard_Service.Get();
                if (dashboard != null)
                {
                    txtServer.Text = ClsStruct.BASE_URL = dashboard.BASE_URL;
                    txtSubpath.Text = ClsStruct.SubPath = dashboard.SubPath;
                    txtRDAID.Text = ClsStruct.Bot_ID = dashboard.Bot_ID;
                    txtNickname.Text = ClsStruct.Bot_Name = dashboard.Bot_Name;
                    //MessageBox.Show($"DB Info :: dbpath :: {ClsStruct.dbPath_} Errors :: {ClsStruct.Errors}");
                }
                else
                {
                    MessageBox.Show("DB Connect Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB Connect Error :: {ex.ToString()}");
            }
            
        }

        private async Task CheckConnectionStateAsync()
        {
            while (connectionRetry)
            {
                if (ClsStruct.hubConnection_.State != HubConnectionState.Connected)
                {
                    //Console.WriteLine("Connection lost");
                    //listBox1.Items.Add("Connection lost");
                    //ShowConnectionLostAlert();
                    await TryReconnectAsync();
                }

                await Task.Delay(5000); // 5초 간격으로 확인
            }
        }

        private async Task TryReconnectAsync()
        {
            while (connectionRetry && connectionTry > tryCount)
            {
                tryCount++;

                try
                {
                    await Task.Delay(CheckInterval); // 5초 대기
                    await ClsStruct.hubConnection_.StartAsync();
                    //Console.WriteLine("Reconnected to the server");
                    listBox1.Items.Insert(0, "Reconnected to the server");
                    break;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Failed to reconnect: {ex.Message}");
                    //listBox1.Items.Add($"Failed to reconnect: try count - {tryCount}");
                }


            }
        }

        private async Task PlayerLiveCheck()
        {
            while (ClsStruct.hubConnection_.State == HubConnectionState.Connected)
            {
                try
                {

                    //if(MessageReceived)
                    //{
                    //    continue;
                    //}

                    string live_ = PlayerCheck();



                    //await this.InvokeAsync(new Action(() =>
                    //{
                    //    CleraListbox();
                    //    string time_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //    listBox1.Items.Insert(0, $"{time_} : {live_}");
                    //}));

                    // UI 업데이트를 별도의 Task에서 실행하여 UI 스레드의 블로킹을 방지
                    if(this.Visible)
                    {
                        await UpdateUIAsync($"{live_}");
                    }
                    
                    if(live_ != latestStatus_Send)
                    {
                        await ClsStruct.hubConnection_.InvokeAsync("SendMessage", ClsStruct.Bot_ID, live_);
                    }

                    latestStatus_Send = live_;

                }
                catch (Exception ex)
                {
                    //listBox1.Items.Add(ex.Message);
                }

                await Task.Delay(CheckInterval); // 초 간격으로 확인
            }

        }

        private string PlayerCheck()
        {
            //BA-Player
            string PlayerName = "BA-Player";
            Process[] aproc = Process.GetProcessesByName(PlayerName);
            if (aproc == null || aproc.Length == 0)
            {
                string AssistName = "BA-Assist";
                Process[] aproc_a = Process.GetProcessesByName(AssistName);
                if(aproc_a !=null && aproc_a.Length > 0)
                {
                    Process proc_batem = aproc_a[0];
                    List<Process> children = new List<Process>();
                    ManagementObjectSearcher mos = new ManagementObjectSearcher(
                        string.Format("Select * from Win32_Process Where ParentProcessID={0}", proc_batem.Id));
                    foreach (ManagementObject mo in mos.Get())
                    {
                        children.Add(Process.GetProcessById(Convert.ToInt32(mo["ProcessID"])));
                    }

                    foreach (Process proc in children)
                    {
                        if (proc.ProcessName == "BA-Player" || proc.ProcessName == "python")
                        {
                            return "Running";
                        }
                    }

                    return "Waiting";
                }
                else
                {
                    return "Waiting";
                }
                
            }
            else
            {
                return "Running";
            }

            
        }

        private async Task UpdateUIAsync(string message, string status = null, Color? color = null)
        {
            if (this.InvokeRequired)
            {
                await this.InvokeAsync(() => PerformUIUpdate(message, status, color));
            }
            else
            {
                PerformUIUpdate(message, status, color);
            }
        }

        private void UpdateUI(string message, string status = null, Color? color = null)
        {
            // UI 업데이트는 반드시 UI 스레드에서 수행해야 하므로 Invoke 사용
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    PerformUIUpdate(message, status, color);
                }));
            }
            else
            {
                PerformUIUpdate(message, status, color);
            }
        }

        private void PerformUIUpdate(string message, string status, Color? color)
        {
            string time_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if(latestStatus != message)
            {
                latestStatus = message;

                listBox1.Items.Insert(0, $"{time_} : {message}");
            }

            if (latestStatus == "Running" || latestStatus == "Waiting")
            {
                
            }
            else
            {
                listBox1.Items.Insert(0, $"{time_} : {message}");
            }
           

            if (status != null)
            {
                lblStatus.Text = status;
            }

            if (color.HasValue)
            {
                lblStatus.ForeColor = color.Value;
            }
        }

        private Task InvokeAsync(Action action)
        {
            return Task.Run(() => this.Invoke(action));
        }

        private async Task ServerMessageParse(string msg)
        {
            WriteQueueInfo(true);

            msg = msg.Replace("[[", "");
            msg = msg.Replace("]]", "");
            string[] arr_msg = msg.Split(';');
            if(arr_msg.Length >= 3 )
            {
                QueueID_ = arr_msg[0];
                TaskName_ = arr_msg[1];
                Filepath_ = arr_msg[2];

                CleraListbox();

                if (this.Visible)
                {
                    await this.InvokeAsync(new Action(() =>
                    {
                        string time_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        listBox1.Items.Insert(0, $"{time_} : {msg}");
                    }));
                }

                await WriteQueueInfo();
                await UpdateQueueTask(TaskName_, QueueID_);
                await QueueTaskRun(TaskName_, QueueID_);
                
            }
            else
            {
                Console.WriteLine($"xxxxxxx yyyyyy :: {msg}");
            }
        }

        private void RegisterHubConnectionEvents()
        {
            if(ClsStruct.hubConnection_ != null)
            {
                ClsStruct.hubConnection_.On<string, string>("ReceiveMessage", async (user, message) =>
                {
                    //if (MessageReceived) return;
                    if(user =="QueueServer")
                    {
                        var newMessage = $"{user} : {message}";
                        string[] arr_msg = message.Split(':');
                        if (!string.IsNullOrEmpty(message))
                        {
                            if (ClsStruct.LogWrite)
                            {
                                string dts_0 = DateTime.Now.ToString("yyyyMMdd");

                                string logPath = Path.Combine(Application.StartupPath, "logs");

                                Directory.CreateDirectory(logPath);
                                logPath = Path.Combine(Application.StartupPath, "logs", $"logFile{dts_0}_{user}.txt");

                                if (!File.Exists(logPath))
                                {
                                    File.WriteAllText(logPath, "Start =======");
                                }

                                string dts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                string txt = $"{dts}  =====  RegisterHubConnectionEvents {user} ::::: {newMessage}";
                                File.AppendAllLines(logPath, new string[] { txt });
                            }


                            //MessageReceived = true;
                            queueServer = user;
                            await ServerMessageParse(message);

                            await Task.Delay(3000); // 비동기 대기를 위해 await 추가
                                                    //MessageReceived = false;
                        }
                    }
                });

                ClsStruct.hubConnection_.Closed += HubConnection_Closed;
            }
            
        }

        private async void CleraListbox()
        {
            this.Invoke(new Action(() =>
            {
                if (listBox1.Items.Count > 20)
                {
                    listBox1.Items.Clear();
                }
            }));

        }

        /// <summary>
        /// 서버에서 받은 Queue 정보를 파일에 기록/삭제
        /// </summary>
        /// <param name="isDelete"></param>
        private async Task WriteQueueInfo(bool isDelete = false)
        {
            string filepath = Path.Combine(Application.StartupPath, "QueueInfo.txt");

            if(isDelete)
            {
                
                File.WriteAllText(filepath, string.Empty);
            }
            else
            {
                File.WriteAllLines(filepath, new string[] {QueueID_, Filepath_ });
            }
        }

        private async Task QueueTaskRun(string taskfile, string queueid)
        {
            string playerPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86);
            playerPath = System.IO.Path.Combine(playerPath, "BA-Assist", "BA-Player.exe");

            string taskfileFullpath = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData)
                ,"BA-Assist","Tasks", taskfile);



            string arg = $"{taskfileFullpath} {"Call"} {queueid}";

            bool result = await RunPlayer(playerPath, arg);
        }

        private async Task<bool> RunPlayer(string fileName, string arguments)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();
                proc.WaitForExit();
                System.Threading.Thread.Sleep(10);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //MessageBox.Show($"File Name : {fileName}, arguments : {arguments}, {ex.ToString()}");
                return false;
            }

        }

        private async Task UpdateQueueTask(string taskfile, string queueid)
        {
            QueueTask task = new QueueTask();
            task.ID = queueid;
            task.TaskFile = taskfile;
            task.Status = "Start";

            //DataHelper.BASE_URL = "https://localhost:7060/";
            //DataHelper.SubPath = "TRM/";
            DataHelper.DetailPath = "updateQueueTask";

            await DataHelper.Update_(task);
        }
    }
}
