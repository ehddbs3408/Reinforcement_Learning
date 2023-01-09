using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforcement_Learning
{
    public class GameParameters
    {
        public static int SteteCount = 19560; //222211110 10진수 표현
        public static int ActionMinIndex = 1;
        public static int ActionMaxIndex = 9;
    }

    class GameState
    {
        public int[,] BoardState;
        public int NextTrun;
        public int BoardStateKey;
        public int NumberOfBlacks;
        public int NumberOfWhites;
        public int GameWinner;

        public GameState()
        {
            BoardState = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            NextTrun = 1;
            BoardStateKey = 1;
            NumberOfBlacks = 0;
            NumberOfWhites = 0;
            GameWinner = 0;
        }

        public void PopulateBoard(int boardState)
        {
            //주어진 보드 상태 값을 3진수로 변환시키면서 보드 상태 생성
            int boardValueProcessing = boardState;
            NumberOfBlacks = 0;
            NumberOfWhites = 0;

            for(int i = 8;i>=0;i--)
            {
                int boardValue = boardValueProcessing % 3;
                boardValueProcessing = boardValueProcessing / 3;

                BoardState[i / 3, i % 3] = boardValue;

                if (boardValue == 1) NumberOfBlacks++;
                if (boardValue == 2) NumberOfWhites++;
            }
        
        }
    }
}
