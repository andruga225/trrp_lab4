using Gamelogic;
using Grpc.Core;
using Grpc.Net.Client;
using lab4.Services;
using Microsoft.AspNetCore.Builder;
using Grpc.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

namespace lab4
{
    public partial class Form1 : Form
    {
        // This will get the current PROJECT directory
        private readonly string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public Image stoneImage;
        public Image scissorsImage;
        public Image paperImage;
        private string address;
        private int port;
        private readonly HttpClientHandler httpHandler = new HttpClientHandler();
        private GrpcChannel serverChannel;
        private GameLogic.GameLogicClient serverClient;
        private readonly string userId;
        private int clientPort;
        GrpListener listener;
        private static string yourScore;
        private static string enemyScore;
        private static ScoreReceiver.MoveType enemyChoice;
        private static ScoreReceiver.GameState gameState;
        UserConnection gameUser;
        private string gameId;
        public bool IsMove { get; set; } = false;
        Task ping = null;
        private static CancellationTokenSource ts = new CancellationTokenSource();
        private CancellationToken ct = ts.Token;
        private readonly string dispatcherAddress = ConfigurationManager.AppSettings["dispatcherAddress"];
        private readonly int dispatherPort = int.Parse(ConfigurationManager.AppSettings["dispatcherPort"]);
        private readonly string clientAddress = ConfigurationManager.AppSettings["clientAddress"];

        public Form1(string user, string address, int port)
        {
            this.address = address;
            this.port = port;
            this.userId = user;

            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            clientPort = int.Parse(FreeTcpPort());
            gameUser = new UserConnection{ Id = userId, Address = clientAddress, Port = clientPort };
            new Task(() => StartGrpcScoreReceiver()).Start();
            new Task(() => InitializeGrpcGameLogic(this.address, this.port)).Start();
            
            InitializeComponent();
            stoneImage = Image.FromFile(projectDirectory + "\\Pictures\\stone.png");
            scissorsImage = Image.FromFile(projectDirectory + "\\Pictures\\scissors.png");
            paperImage = Image.FromFile(projectDirectory + "\\Pictures\\paper.png");
            btnPaper.Enabled = false;
            btnShear.Enabled = false;
            btnStone.Enabled = false;

        }

        public Button ButtonStone
        {
            get { return btnStone; }
        }

        public Button ButtonPaper
        {
            get { return btnPaper; }
        }

        public Button ButtonSnear
        {
            get { return btnShear; }
        }

        public Label LabelYourScore
        {
            get { return labelyourScore; }
        }

        public Label LabelEnemyScore
        {
            get { return labelEnemyScore; }
        }

        public PictureBox PictureEnemyChoice
        {
            get { return picEnemyChoice; }
        }

        public PictureBox PictureYourChoice
        {
            get { return picYourChoice; }
        }

        private static string FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port.ToString();
        }

        private bool InitializeGrpcGameLogic(string address, int port)
        {

            try
            {
				 serverChannel = GrpcChannel.ForAddress($"http://{address}:{port}",
					new GrpcChannelOptions { HttpHandler = httpHandler });
				serverClient = new GameLogic.GameLogicClient(serverChannel);
                Gamelogic.ConnectionStatus success = serverClient.connect(gameUser);
                if(!success.IsConnected)
                {
                    MessageBox.Show("Данный сервер переполнен. Выберите другой.");
                    Close();
                }

                gameId = success.GameId;
            }
            catch(RpcException ex)
            {
                MessageBox.Show("Связь с сервером потеряна");
                Close();
            }

			Task.Run(() => PingServer(), ct);
				
            return true;
        }

        private void StartGrpcScoreReceiver()
        {
            listener = new GrpListener(this);
            listener.Start(clientAddress, clientPort);
        }

        private async void PingServer()
        {
            while (true)
            {
                if (ct.IsCancellationRequested){
					ts = new CancellationTokenSource();
					ct = ts.Token;
                    return;
				}
                try
                {
                    _ = serverClient.ping(new Empty());
                }
                catch (RpcException ex)
                {
                    Invoke(() => {
                        btnPaper.Enabled = false;
                        btnShear.Enabled = false;
                        btnStone.Enabled = false;
                    });

                    bool result = await Reconnect();
                    if (!result)
                    {
                        MessageBox.Show("Ваш сервер не доступен, и не нашлось свободных для продолжения игры:(");
                        if(InvokeRequired)
                        {
                            Invoke(() => { Close(); });
                        }
                        else
                        {
                            Close();
                        }
                        return;
                    }
					return;


                }
                Thread.Sleep(500);
            }
        }

        private async Task<bool> Reconnect()
        {
            // Return `true` to allow certificates that are untrusted/invalid
            try
            {
                using var channel = GrpcChannel.ForAddress($"http://{dispatcherAddress}:{dispatherPort}",
                    new GrpcChannelOptions { HttpHandler = httpHandler, });

                var client = new Serversinfo.ServersInfo.ServersInfoClient(channel);

                var newServer = client.ServerConnectionLost(new Serversinfo.ConnectionLostInfo{ GameId=gameId, UserId=userId});

                if(newServer.IsExists)
                {
                    address = newServer.Address;
                    port = newServer.Port;
                    await Task.Run(() => InitializeGrpcGameLogic(address, port));
                    return await Task.FromResult(true);
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        private async Task SendChoiceAsync(MoveType move)
        {
            /*
             * Отправка выбора на сервер строкой состояния (идент,выбор)
             * Ожидание ответа сервера
             */
            try
            {
                serverClient.makeMove(new Move { Move_ = move, UserId = userId });
                IsMove = true;
                btnPaper.Enabled = false;
                btnShear.Enabled = false;
                btnStone.Enabled = false;
            }
            catch
            {
                bool result = await Reconnect();
                if (!result)
                {
                    MessageBox.Show("Ваш сервер не доступен, и не нашлось свободных для продолжения игры:(");
                    if (InvokeRequired)
                    {
                        Invoke(() => { Close(); });
                    }
                    else
                    {
                        Close();
                    }
                    return;
                }
            }

            return;
        }

        public void stopListener()
        {
            listener.Stop();
        }


        private void btnPaper_Click(object sender, EventArgs e)
        {
            picYourChoice.Image = paperImage;
            SendChoiceAsync(MoveType.Paper);
        }

        private void btnStone_Click(object sender, EventArgs e)
        {
            picYourChoice.Image = stoneImage;
            SendChoiceAsync(MoveType.Rock);
        }

        private void btnShear_Click(object sender, EventArgs e)
        {
            picYourChoice.Image = scissorsImage;
            SendChoiceAsync(MoveType.Scissors);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ts.Cancel();
            stopListener();
            try
            {
                serverClient.disconnect(gameUser);
            }
            catch (RpcException) { }
            
            
        }
    }
}