using Grpc.Core;
using Grpc.Core.Utils;
using Serversinfo;
using ServerDispatcher;
using User = Serversinfo.User;

namespace Dispatcher
{
    public class ServersInfoImpl : ServersInfo.ServersInfoBase
    {
        private static Dictionary<string, NewServerInfo> serverByGame = new Dictionary<string, NewServerInfo>();

        public override Task<NewServerInfo> ServerConnectionLost(ConnectionLostInfo request, ServerCallContext context)
        {
            string gameId = request.GameId;
            string userId = request.UserId;

            lock (serverByGame) // mb race condition
            {
                if (!serverByGame.ContainsKey(gameId))
                {
                    NewServerInfo newServerInfo = new NewServerInfo { IsExists = false };
                    var listServerConfigs = Dispatcher.GetListServersConfigs();
                    foreach (var serverConfig in listServerConfigs)
                    {
                        var serverInfo = new ServerInfo
                        {
                            Address = serverConfig.Address,
                            Port = serverConfig.GrpcPort
                        };

                        // Пытаемся достучаться до каждого сервера и получить загруженность. Если не получается, то удаляем из списка серверов
                        try
                        {
                            Channel channel = new Channel($"{serverConfig.Address}:{serverConfig.GrpcPort}", ChannelCredentials.Insecure);
                            var client = new ServerDispatcher.ServerDispatcher.ServerDispatcherClient(channel);
                            var result = client.GetCongestion(new ServerDispatcher.Empty());
                            if (result.Congestion_ == 0) // found empty server
                            {
                                newServerInfo.Address = serverConfig.Address;
                                newServerInfo.Port = serverConfig.GrpcPort;
                                newServerInfo.IsExists = true;
                                Console.WriteLine("Found!");
                                Console.WriteLine(serverConfig.GrpcPort);
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Dispatcher.RemoveServer(serverConfig.Address, serverConfig.Port);
                            Console.WriteLine($"Не удалось подключиться к серверу: {serverConfig.Address}:{serverConfig.GrpcPort}");
                        }
                    }
                    Console.WriteLine("Updated");
                    serverByGame[gameId] = newServerInfo;
                }
            }

            var newServerConfig = serverByGame[gameId];
            Console.WriteLine($"Lost Connection | {userId} | {gameId} | {newServerConfig.Address}:{newServerConfig.Port} | {newServerConfig.IsExists}");
            Channel newServerChannel = new Channel($"{newServerConfig.Address}:{newServerConfig.Port}", ChannelCredentials.Insecure);
            var newServerClient = new ServerDispatcher.ServerDispatcher.ServerDispatcherClient(newServerChannel);
            var isOk = newServerClient.RestoreUserState(new ServerDispatcher.User { Id = userId });
            newServerConfig.IsExists = isOk.Value;

            return Task.FromResult(newServerConfig);
        }
        public override Task<User> GetUserId(Serversinfo.Empty request, ServerCallContext context)
        {
            return Task.FromResult(new User() { Id = Guid.NewGuid().ToString() });
        }
        public override async Task GetServersInfo(Serversinfo.Empty empty, IServerStreamWriter<ServerInfo> responseStream, ServerCallContext context)
        {
            var listServerConfigs = Dispatcher.GetListServersConfigs();
            foreach (var serverConfig in listServerConfigs)
            {
                var serverInfo = new ServerInfo
                {
                    Address = serverConfig.Address,
                    Port = serverConfig.GrpcPort
                };

                // Пытаемся достучаться до каждого сервера и получить загруженность. Если не получается, то удаляем из списка серверов
                try
                {
                    Channel channel = new Channel($"{serverConfig.Address}:{serverConfig.GrpcPort}",
                        ChannelCredentials.Insecure);
                    var client = new ServerDispatcher.ServerDispatcher.ServerDispatcherClient(channel);
                    var result = client.GetCongestion(new ServerDispatcher.Empty());
                    serverInfo.Congestion = result.Congestion_;
                    await responseStream.WriteAsync(serverInfo);
                }
                catch (Exception ex)
                {
                    Dispatcher.RemoveServer(serverConfig.Address, serverConfig.Port);
                    Console.WriteLine($"Не удалось подключиться к серверу: {serverConfig.Address}:{serverConfig.GrpcPort}");
                }

            }
        }
    }
}