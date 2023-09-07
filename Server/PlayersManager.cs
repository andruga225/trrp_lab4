using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamelogic;
using MoveType = Gamelogic.MoveType;
using static Server.RPSLogic;
using Grpc.Net.Client;
using Grpc.Core;
using ServerDispatcher;
using System.Reflection.Metadata.Ecma335;

namespace Server
{
    class PlayersManager
    {
        private static List<string> users = new List<string>();
        private static Dictionary<string, int> scores = new Dictionary<string, int>();
        private static Dictionary<string, MoveType> moves = new Dictionary<string, MoveType>();
        private static Dictionary<string, string> usersAddress = new Dictionary<string, string>();
        private static bool Restoring = false;
        private static int gameId = 1;

        public static bool restoreUserState(User user)
        {
            Restoring = true;
            DBManager.UserState game;
            game.score = 0;
            game.choice = null;

            try
            {
                game = DBManager.getUserState(user.Id);
            }
            catch
            {
                return false;
            }

            lock (scores)
            {
                scores[user.Id] = game.score;
                Console.WriteLine($"Restore score: {user.Id} {game.score}");
            }

            lock (moves)
            {
                if (game.choice != null)
                    moves[user.Id] = (MoveType)game.choice;
                Console.WriteLine($"Restore choice: {user.Id} {game.choice}");
            }

            Console.WriteLine($"RestoreUserState | {user.Id} | {game.score} | {game.choice}");
            return true;
        }

        public static string getGameId()
        {
            return $"{Server.ServerAddress}-{gameId}";
        }

        public static int usersCount()
        {
            return users.Count;
        }
        public static bool connectUser(UserConnection userConnection)
        {
            string userId = userConnection.Id;
            Console.WriteLine(userId);
            lock (users)
            {
                if (users.Count >= 2)
                {
                    return false;
                }

                users.Add(userId);
                Console.WriteLine($"New user: {userId}, address: {userConnection.Address}:{userConnection.Port} | Users Count: {users.Count}");
                lock (usersAddress)
                {
                    usersAddress.Add(userId, $"{userConnection.Address}:{userConnection.Port}");
                }

                if (users.Count == 2)
                {
                    
                    users.ForEach(async user =>
                    {
                        if (!Restoring)
                            DBManager.InsertUser(user);
                        string address = usersAddress[user];
                        Console.WriteLine(address);
                        Channel channel = new Channel(address, ChannelCredentials.Insecure);
                        var client = new ScoreReceiver.ScoreState.ScoreStateClient(channel);
                        await client.gameStartAsync(new ScoreReceiver.Empty());
                    });
                    if (Restoring) return true;
                    lock (scores)
                    {
                        scores.Clear();
                        users.ForEach(userId =>
                        {
                            scores[userId] = 0;
                        });
                    }

                    lock (moves)
                    {
                        moves.Clear();
                    }
                }
            }

            return true;
        }

        public static void userDisconnect(UserConnection userConnection)
        {
            Restoring = false;
            lock (users)
            {
                DBManager.Delete(userConnection.Id);
                users.Remove(userConnection.Id);
                if (users.Count == 1)
                {
                    try
                    {
                        var client = new ScoreReceiver.ScoreState.ScoreStateClient(new Channel(usersAddress[users[0]], ChannelCredentials.Insecure));
                        client.clientDisconnectAsync(new ScoreReceiver.Empty());
                    }
                    catch (Exception) { }
                }
            }

            lock (usersAddress)
            {
                usersAddress.Remove(userConnection.Id);
            }
        }

        public static void makeMove(Move userMove)
        {
            string userId = userMove.UserId;
            MoveType move = userMove.Move_;

            lock (moves)
            {
                moves[userId] = move;
                DBManager.InsertChoice(userId, move);

                if (moves.Count == 2)
                {
                    string playerA = users[0];
                    string playerB = users[1];

                    MoveType moveA = moves[playerA];
                    MoveType moveB = moves[playerB];

                    State stateA, stateB;
                    (stateA, stateB) = getState(moveA, moveB);

                    bool isEnd = false;

                    var gameStateA = ScoreReceiver.GameState.Lose;
                    var gameStateB = ScoreReceiver.GameState.Lose;

                    DBManager.NullChoices(playerA, playerB);

                    lock (scores)
                    {
                        if (stateA == State.Win)
                        {
                            scores[playerA]++;
                            DBManager.IncrementScore(playerA);
                            gameStateA = ScoreReceiver.GameState.Win;
                        }

                        if (stateB == State.Win)
                        {
                            scores[playerB]++;
                            DBManager.IncrementScore(playerB);
                            gameStateB = ScoreReceiver.GameState.Win;
                        }

                        if (scores[playerA] == 3 || scores[playerB] == 3)
                        {
                            isEnd = true;
                            DBManager.Delete(users[0], users[1]);
                            Restoring = false;
                        }
                    }

                    var clientA = new ScoreReceiver.ScoreState.ScoreStateClient(new Channel(usersAddress[playerA], ChannelCredentials.Insecure));
                    var clientB = new ScoreReceiver.ScoreState.ScoreStateClient(new Channel(usersAddress[playerB], ChannelCredentials.Insecure));

                    if (!isEnd)
                    {
                        gameStateA = ScoreReceiver.GameState.NotResult;
                        gameStateB = ScoreReceiver.GameState.NotResult;
                    }

                    Task.Run(async () => await clientA.sendScoreAsync(new ScoreReceiver.Score()
                    {
                        Score_ = $"{scores[playerA]}:{scores[playerB]}",
                        EnemyChoice = (ScoreReceiver.MoveType)moveB,
                        Result = gameStateA
                    }));
                    Task.Run(async () => await clientB.sendScoreAsync(new ScoreReceiver.Score()
                    {
                        Score_ = $"{scores[playerB]}:{scores[playerA]}",
                        EnemyChoice = (ScoreReceiver.MoveType)moveA,
                        Result = gameStateB
                    }));

                    moves.Clear();
                }
            }
        }
    }
}
