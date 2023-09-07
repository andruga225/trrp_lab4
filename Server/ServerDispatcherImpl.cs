using ServerDispatcher;
using Grpc.Core;
using Server;

public class ServerDispatcherImpl : ServerDispatcher.ServerDispatcher.ServerDispatcherBase
{
    public override Task<Congestion> GetCongestion(Empty empty, ServerCallContext context)
    {
        return Task.FromResult(new Congestion { Congestion_ = Server.PlayersManager.usersCount() });
    }
    public override Task<BoolValue> RestoreUserState(User request, ServerCallContext context)
    {
        return Task.FromResult(new BoolValue { Value = PlayersManager.restoreUserState(request) });
    }
}