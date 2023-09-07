using ServerDispatcher;
using Grpc.Core;
using Server;

public class ServerListener
{
    private Grpc.Core.Server server;

    public void Start(string iPAddress, int port)
    {
        server = new Grpc.Core.Server
        {
            Services = { ServerDispatcher.ServerDispatcher.BindService(new ServerDispatcherImpl()), Gamelogic.GameLogic.BindService(new GameLogicImplemintation()) },
            Ports = { new ServerPort(iPAddress, port, ServerCredentials.Insecure) }
        };
        server.Start();
    }

    public void Stop()
    {
        server.ShutdownAsync().Wait();
    }
}