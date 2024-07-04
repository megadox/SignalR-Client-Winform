using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalR_Client
{
    public partial class Form1 : Form
    {

        HubConnection hubConnection;
        bool connectionRetry = true;
        public Form1()
        {
            InitializeComponent();

            this.btnStartConn.Click += BtnStartConn_Click1;
            this.btnSend.Click += BtnSend_Click;
            this.btnStopConn.Click += BtnStopConn_Click;

            hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri("https://localhost:7060/ChatHub"))
                .Build();

            hubConnection.Closed += HubConnection_Closed;
            

            this.Load += Form1_Load;

        }

        private async void BtnStartConn_Click1(object sender, EventArgs e)
        {
            connectionRetry = true;
            tryCount = 0;

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user} : {message}";
                this.Invoke(new Action(() =>
                {
                    listBox1.Items.Add(newMessage);
                }));

            });
            //hubConnection.On<string>("UserDisconnectecd", (user) =>
            //{
            //    this.Invoke(new Action(() =>
            //    {
            //        listBox1.Items.Add("Server disconnected.");
            //    }));
            //});
            
            try
            {
                await hubConnection.StartAsync();
                await hubConnection.InvokeAsync("Register", txtName.Text);
                listBox1.Items.Add("Connection Started.");
                
                 CheckConnectionStateAsync();
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private async Task CheckConnectionStateAsync()
        {
            while (connectionRetry)
            {
                if (hubConnection.State != HubConnectionState.Connected)
                {
                    //Console.WriteLine("Connection lost");
                    listBox1.Items.Add("Connection lost");
                    //ShowConnectionLostAlert();
                    await TryReconnectAsync();
                }

                await Task.Delay(5000); // 5초 간격으로 확인
            }
        }

        //private void ShowConnectionLostAlert()
        //{
        //    // 연결 끊김 알림 표시
        //    // 예: UI 요소 업데이트, 모달 창 표시 등
        //    //Console.WriteLine("Connection lost alert displayed");
        //    listBox1.Items.Add("Connection lost alert displayed");
        //}

        int connectionTry = 10;
        int tryCount = 0;

        private async Task TryReconnectAsync()
        {
            while (connectionRetry && connectionTry > tryCount)
            {
                tryCount++;

                try
                {
                    await Task.Delay(5000); // 5초 대기
                    await hubConnection.StartAsync();
                    //Console.WriteLine("Reconnected to the server");
                    listBox1.Items.Add("Reconnected to the server");
                    break;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Failed to reconnect: {ex.Message}");
                    listBox1.Items.Add($"Failed to reconnect: try count - {tryCount}");
                }

               
            }
        }

        private async void BtnStopConn_Click(object sender, EventArgs e)
        {
            tryCount = 0;
            connectionRetry = false;
            hubConnection.DisposeAsync();

            //try
            //{
            //    await hubConnection.InvokeAsync("SendMessage", txtName.Text, txtClientMsg.Text);
            //}
            //catch (Exception ex)
            //{
            //    listBox1.Items.Add(ex.Message);
            //}
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", txtName.Text, txtClientMsg.Text);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private async Task HubConnection_Closed(Exception arg)
        {


            listBox1.Items.Add("Connection Closed.");
            //await Task.Delay(new Random().Next(0,5) * 1000);
            //await hubConnection.StartAsync();
        }

    }
}
