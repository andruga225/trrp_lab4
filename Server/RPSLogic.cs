using Gamelogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class RPSLogic
    {
        public enum State
        {
            Win,
            Lose,
            Draw
        }

        private static Dictionary<MoveType, MoveType> losesTo = new Dictionary<MoveType, MoveType>()
        {
            { MoveType.Paper, MoveType.Scissors },
            { MoveType.Rock, MoveType.Paper },
            { MoveType.Scissors, MoveType.Rock },
        };

        public static (State, State) getState(MoveType moveA, MoveType moveB) 
        {
            if (moveB == losesTo[moveA])
                return (State.Lose, State.Win);
            if (moveA == losesTo[moveB])
                return (State.Win, State.Lose);
            return (State.Draw, State.Draw);
        }
    }
}
