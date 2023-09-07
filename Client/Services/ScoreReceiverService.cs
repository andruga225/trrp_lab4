using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using ScoreReceiver;

namespace lab4.Services
{
    internal class ScoreReceiverService: ScoreState.ScoreStateBase
    {
        public Form1 game;
        private bool isEndGame = false;

        public ScoreReceiverService(Form1 game)
        {
            this.game = game;
        }


        public override Task<Empty> clientDisconnect(Empty request, ServerCallContext context)
        {
            if (!isEndGame)
                game.Invoke(() =>
                {
                    game.IsMove = false;
                    MessageBox.Show($"Соперник вышел из игры. Вы победили!");
                    game.Close();
                });
            return Task.FromResult(new Empty());
        }
        public override Task<Empty> sendScore(Score request, ServerCallContext context)
        {
            game.Invoke(() =>
            {
                game.IsMove = false; 
                game.LabelYourScore.Text = request.Score_[0].ToString();
                game.LabelEnemyScore.Text = request.Score_[2].ToString();

                switch (request.EnemyChoice)
                {
                    case MoveType.Rock:
                        game.PictureEnemyChoice.Image = game.stoneImage; break;
                    case MoveType.Paper:
                        game.PictureEnemyChoice.Image = game.paperImage; break;
                    case MoveType.Scissors:
                        game.PictureEnemyChoice.Image = game.scissorsImage; break;
                }

                switch (request.Result)
                {
                    case GameState.Win:
                        isEndGame = true;
                        MessageBox.Show($"Вы победили со счетом {game.LabelYourScore.Text}:{game.LabelEnemyScore.Text}");
                        game.Close();
                        break;
                    case GameState.Lose:
                        isEndGame = true;
                        MessageBox.Show($"Вы проиграли со счетом {game.LabelYourScore.Text}:{game.LabelEnemyScore.Text}");
                        game.Close();
                        break;
                    case GameState.NotResult:
                        MessageBox.Show($"Раунд завершен");
                        //game.Close();
                        break;
                }

                game.ButtonPaper.Enabled = true;
                game.ButtonSnear.Enabled = true;
                game.ButtonStone.Enabled = true;

                game.PictureEnemyChoice.Image = null;
                game.PictureYourChoice.Image = null;
            });

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> gameStart(Empty request, ServerCallContext context)
        {
            isEndGame = false;
            while (!game.IsHandleCreated) ;
            game.Invoke(() =>
            {
                if (game.IsMove) return;
                game.ButtonPaper.Enabled = true;
                game.ButtonSnear.Enabled = true;
                game.ButtonStone.Enabled = true;
            });

            return Task.FromResult(new Empty());
        } 
    }
}
