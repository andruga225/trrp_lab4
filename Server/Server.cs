using System.Net.Sockets;
using System.Net;
using System.Configuration;
using System.Text;
using ServerDispatcher;
using Grpc.Core;
using Newtonsoft.Json;
using Gamelogic;

namespace Server
{
    class Server
    {

        public static string ServerAddress;
        // Подключение к диспетчеру
        private static async Task<bool> DispatcherConnection(string iPAddress, int port, string[] sendingData)
        {
            bool result = true;
            using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                await tcpClient.ConnectAsync(iPAddress, port);
                var serializableMessage = JsonConvert.SerializeObject(sendingData);
                var messageBytes = Encoding.UTF8.GetBytes(serializableMessage);
                await tcpClient.SendAsync(messageBytes, SocketFlags.None);
                Console.WriteLine("Соединение с диспетчером установлено.");
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine("Произошла ошибка при подключении к диспетчеру.");
                
            }
            return result;
        }
        
        // Поиск свободного порта для grpc
        private static string FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port.ToString();
        }

        // Получение данных из файла конфигурации
        private static (string?, string?, string?) GetConfigData()
        {
            var grpcIpAddress = ConfigurationManager.AppSettings["grpcAddress"];
            var dispatcherIpAddress = ConfigurationManager.AppSettings["dispatcherAddress"];
            var dispatcherPort = ConfigurationManager.AppSettings["dispatcherPort"];
            return (grpcIpAddress, dispatcherIpAddress, dispatcherPort);
        }
        
        public static async Task Main(string[] args)
        {
            var (grpcIpAddress, dispatcherIpAddress, dispatcherPort) = GetConfigData();
            if (grpcIpAddress is null || dispatcherIpAddress is null || dispatcherPort is null)
            {
                Console.WriteLine("Не удалось найти данные в файле конфигурации.");
                return;
            }
            
            // Передача данных на диспетчер. Пока что только порт grpc. IP диспетчер получает сам при подключении клиента
            string grpcPort = FreeTcpPort();
            string[] sendingData = { grpcPort, "<|EOM|>" };
            bool resultConnection = await DispatcherConnection(dispatcherIpAddress, int.Parse(dispatcherPort), sendingData);
            
            // Если соединение с диспетчером не было установлено. Возможно, добавить ещё попытки на другие
            if (!resultConnection)
                return;

            // DB Init
            DBManager.Init();

            ServerAddress = $"{grpcIpAddress}:{grpcPort}";
            // Запуск прослушки запросов от диспетчера
            var dispatcherListener = new ServerListener();
            Console.WriteLine(grpcIpAddress);
            Console.WriteLine(grpcPort);

            dispatcherListener.Start(grpcIpAddress, int.Parse(grpcPort));
            
            Console.WriteLine("Нажмите любую клавишу, чтобы остановить сервер...");
            Console.ReadKey();
            dispatcherListener.Stop();

        }
    }
}