using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;
using Dispatcher;
using Grpc.Core;
using Serversinfo;

namespace Dispatcher
{
    class Dispatcher
    {
        // Список доступных серверов
        private static List<ServerConfig>  listServerConfigs = new List<ServerConfig>();
  
        // Добавление новых серверов
        private static async Task ServerConnection(IPAddress iPAddress, int port)
        {
            IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);
            using Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                tcpListener.Bind(ipPoint);
                tcpListener.Listen();
                Console.WriteLine("Диспетчер запущен. Ожидание подключений... ");
                while (true)
                {
                    using var tcpClient = await tcpListener.AcceptAsync();
                    
                    var buffer = new byte[1024];
                    var received = await tcpClient.ReceiveAsync(buffer, SocketFlags.None);
                    var response = Encoding.UTF8.GetString(buffer, 0, received);
                    var receivedObject = JsonConvert.DeserializeObject<string[]>(response);
                    var eom = "<|EOM|>";
                    if (receivedObject[1] == eom)
                    {
                        lock (listServerConfigs)
                        {
                            listServerConfigs.Add(new ServerConfig(
                                ((IPEndPoint)tcpClient.RemoteEndPoint).Address.ToString(),
                                int.Parse(((IPEndPoint)tcpClient.RemoteEndPoint).Port.ToString()),
                                int.Parse(receivedObject[0])
                            ));
                        }
                        Console.WriteLine($"Добавлен новый сервер: адрес = {((IPEndPoint)tcpClient.RemoteEndPoint).Address.ToString()}, порт = {int.Parse(receivedObject[0])}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<ServerConfig> GetListServersConfigs()
        {
            var resultList = new List<ServerConfig>();
            lock (listServerConfigs)
            {
                foreach (var serverConfig in listServerConfigs)
                    resultList.Add(serverConfig);
            }
            
            return resultList;
        }

        public static void RemoveServer(string ipAdress, int port)
        {
            lock (listServerConfigs)
            {
                listServerConfigs.RemoveAll(x => x.Address == ipAdress && x.Port == port);
            }
        }
        
        private static void StartServerListener()
        {
            var iPAddress = ConfigurationManager.AppSettings["socketAddress"];
            var port = ConfigurationManager.AppSettings["socketPort"];
            if (iPAddress is null || port is null)
            {
                Console.WriteLine("Не удалось найти адрес и порт в файле конфигурации.");
                return;
            }
            ServerConnection(IPAddress.Parse(iPAddress), int.Parse(port));
        }

        private static void StartClientListener()
        {
            var iPAddress = ConfigurationManager.AppSettings["grpcAddress"];
            var port = ConfigurationManager.AppSettings["grpcPort"];
            
            if (iPAddress is null || port is null)
            {
                Console.WriteLine("Не удалось найти адрес и порт в файле конфигурации.");
                return;
            }
            
            Server server = new Server
            {
                Services = { ServersInfo.BindService(new ServersInfoImpl()) },
                Ports = { new ServerPort(iPAddress, int.Parse(port), ServerCredentials.Insecure) }
            };
            server.Start();
            
            Console.WriteLine("Нажмите любую клавишу, чтобы остановить диспетчер...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
        
        public static void Main(string[] args)
        {
            StartServerListener();
            StartClientListener();
        }
    }
}

