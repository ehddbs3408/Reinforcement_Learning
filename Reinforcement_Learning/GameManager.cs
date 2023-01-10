using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforcement_Learning
{
    public enum GamePlayer
    {
        DynamicProgramming,
        Human,
        None
    }
    class GameManager
    {
        public GamePlayer BlackPlayer;
        public GamePlayer WhitePlayer;
        public void PlayGame()
        {
            while(true)
            {

                BlackPlayer = GetBlackPlayer();
                if (BlackPlayer == GamePlayer.None) return;


                WhitePlayer = GetWhitePlayer();
                if (WhitePlayer == GamePlayer.None) return;

                ManageGame();
            }
        }
        private GamePlayer GetBlackPlayer()
        {
            return PlayerSelection("x ");
        }
        private GamePlayer GetWhitePlayer()
        {
            return PlayerSelection("o ");
        }

        public GamePlayer PlayerSelection(string menuLabel)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(menuLabel);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1) 동적 프로그래밍");
                Console.WriteLine("2) 사람");
                Console.WriteLine("3) 게임 종료");
                Console.WriteLine("선택 (1~3)");

                switch(Console.ReadLine())
                {
                    case "1":
                        if(Program.DPManager.StateValueFunction.Count > 0)
                        {
                            return GamePlayer.DynamicProgramming;
                        }
                        else
                        {
                            Console.WriteLine("동적프로그래밍 수행하세요");
                            Console.WriteLine(Environment.NewLine);
                        }

                        break;

                        
                    case "2":
                        return GamePlayer.Human;
                    case "3":
                        return GamePlayer.None;
                    default:
                        break;
                }
            }
        }

        public void ManageGame()
        {
            GameState gameState = new GameState();
            int gameTurnCount = 0;
            int gameMove = 0;

            bool isGameFinished = gameState.isFinalState();

            while(!isGameFinished)
            {
                gameState.DisplayBoard(gameTurnCount, gameMove, BlackPlayer, WhitePlayer);

                isGameFinished = gameState.isFinalState();

                Console.WriteLine(Environment.NewLine);

                if(isGameFinished)
                {
                    Console.WriteLine("게임이 끝났습니다. 아무 키나 눌러 주세요");
                    Console.ReadLine();
                }else
                {
                    GamePlayer playerforNextTurn = GetGamePlayer(gameState.NextTurn);
                    if(playerforNextTurn == GamePlayer.Human)
                    {
                        gameMove = GetHumanGameMove(gameState);
                    }
                    else
                    {
                        Console.Write("아무키나 눌르세요");
                        Console.ReadLine();

                        gameMove = Program.DPManager.GetNextMove(gameState.BoardStateKey);
                    }

                    gameState.MakeMove(gameMove);
                    gameTurnCount++;
                }
            }
        }

        public int GetHumanGameMove(GameState gameState)
        {
            Console.WriteLine("다음 행동을 입력하세여(1~9) : ");
            string humanMove = Console.ReadLine();

            while(true)
            {
                try
                {
                    int gameMove = Int32.Parse(humanMove);

                    if (gameMove >= 1 && gameMove <= 9 && gameState.IsValidMove(gameMove))
                        return gameMove;
                    else
                    {
                        Console.WriteLine("-.- !! (1~9)");
                        humanMove = Console.ReadLine();
                    }
                }
                catch
                {

                }
            }
        }

        public GamePlayer GetGamePlayer(int Turn)
        {
            if (Turn == 1) return BlackPlayer;
            else return WhitePlayer;
        }
    }
}
