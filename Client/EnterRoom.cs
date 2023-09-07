using Grpc.Net.Client;
using Google.Protobuf;
using Grpc.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grpc.Core;
using Gamelogic;
using Serversinfo;
using System.Configuration;

namespace lab4
{
    public partial class EnterRoom : Form
    {
        private string? userId = null;
        private Form1 Game;
        private readonly string dispatcherAddress = ConfigurationManager.AppSettings["dispatcherAddress"];
        private readonly int dispatherPort = int.Parse(ConfigurationManager.AppSettings["dispatcherPort"]);

        public EnterRoom()
        {
            InitializeComponent();

            btnUpdateServerList.Enabled = false;

            GetServers();
            
        }

        private void GetId()
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress($"http://{dispatcherAddress}:{dispatherPort}",
                new GrpcChannelOptions { HttpHandler = httpHandler, });

            var client = new Serversinfo.ServersInfo.ServersInfoClient(channel);

            try
            {
                var response = client.GetUserId(new Serversinfo.Empty());
                userId = response.Id;
            }
            catch
            {
                Close();
            }
        }

        private async void GetServers()
        {
            btn_connect.Enabled = false;
            ServerList.Items.Clear();

            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress($"http://{dispatcherAddress}:{dispatherPort}",
                new GrpcChannelOptions { HttpHandler = httpHandler,  });

            var client = new Serversinfo.ServersInfo.ServersInfoClient(channel);

            try
            {
                var serverData = client.GetServersInfo(new Serversinfo.Empty(), deadline: DateTime.UtcNow.AddSeconds(5));

                var responseStream = serverData.ResponseStream;

                await foreach (var response in responseStream.ReadAllAsync())
                {
                    //Serversinfo.ServerInfo response = responseStream.Current;
                    ServerData server = new ServerData
                    {
                        address = response.Address,
                        port = response.Port,
                        roomNumber = ServerList.Items.Count + 1,
                        userInRoom = response.Congestion

                    };

                    ServerList.Items.Add(server);
                }

                if (userId == null)
                    GetId();
            }catch(RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded || ex.StatusCode == StatusCode.Unavailable)
            {
                MessageBox.Show("Отсутствует связь с сервером");
                btnUpdateServerList.Enabled = true;
                return;
            }

            if (ServerList.Items.Count == 0)
            {
                MessageBox.Show("В настояший момент нет доступных серверов");
                btn_connect.Enabled= false;
            }
            else
            {
                btn_connect.Enabled= true;
            }

            btnUpdateServerList.Enabled = true;
        }

        private void ServerList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                object item = ServerList.Items[e.Index];
                e.DrawBackground();
                e.DrawFocusRectangle();
                Brush brush = new SolidBrush(e.ForeColor);
                SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));
            }

        }

        private void btnUpdateServerList_Click(object sender, EventArgs e)
        {
            btnUpdateServerList.Enabled= false;
            GetServers();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (ServerList.SelectedItems.Count > 0)
            {
                ServerData server = (ServerData)ServerList.SelectedItems[0];

                if(server.userInRoom==2)
                {
                    MessageBox.Show("В этой комнате достигнут лимит игроков");
                    return;
                }

                Game = new Form1(userId, server.address, server.port);

                this.Hide();
                Game.ShowDialog();
                userId = null;
                GetServers();
                this.Show();
            }
            else
            {
                MessageBox.Show("Вы не выбрали комнату для игры");
            }
        }
    }
}
