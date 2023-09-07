using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamelogic;
using Grpc.Core;
using ScoreReceiver;
using static Server.RPSLogic;
using Empty = Gamelogic.Empty;
using MoveType = Gamelogic.MoveType;

namespace Server
{
    public class GameLogicImplemintation: GameLogic.GameLogicBase
    {
        public override Task<ConnectionStatus> connect(UserConnection request, ServerCallContext context)
        {
            var gameId = PlayersManager.getGameId();
            return Task.FromResult(new ConnectionStatus { IsConnected = PlayersManager.connectUser(request), GameId = gameId });
        }

        public override Task<Empty> disconnect(UserConnection request, ServerCallContext context)
        {
            PlayersManager.userDisconnect(request);

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> makeMove(Move request, ServerCallContext context)
        {
            PlayersManager.makeMove(request);

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ping(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }
    }
}
