using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reinforcement_Learning
{
    public class ConnectedPosition
    {
        public int row;
        public int col;
        public ConnectedPosition(int r,int c)
        {
            row = r;
            col = c;
        }
    }
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
        internal int GetFirstStageTurn()
        {
            if (NumberOfWhites == NumberOfBlacks) return 1;
            if (NumberOfBlacks == NumberOfWhites + 1) return 2;

            return 0;
        }

        internal bool IsValidFirsStage()
        {
            if (NumberOfBlacks > 4) return false;
            if (NumberOfWhites > 3) return false;

            if(NumberOfWhites == NumberOfBlacks || NumberOfBlacks == NumberOfWhites + 1)
            {
                return true;
            }

            return false;
        }

        internal bool IsValidSecondStage()
        {
            if (NumberOfBlacks == 4 && NumberOfWhites == 4) return true;
            return false;
        }

        public bool isFinalState()
        {
            GameWinner = 0;

            for (int i = 0; i < 3; i++)
            {
                if (BoardState[i, 0] == BoardState[i, 1] && BoardState[i, 1] == BoardState[i, 2])
                {
                    if (BoardState[i, 0] != 0)
                    {
                        GameWinner = BoardState[i, 0];
                        return true;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (BoardState[0, i] == BoardState[1, i] && BoardState[1, i] == BoardState[2, i])
                {
                    if (BoardState[0, i] != 0)
                    {
                        GameWinner = BoardState[i, 0];
                        return true;
                    }
                }
            }

            if (BoardState[0, 2] == BoardState[1, 1] && BoardState[1, 1] == BoardState[2, 2])
            {
                if (BoardState[0, 2] != 0)
                {
                    GameWinner = BoardState[0, 0];
                    return true;
                }
            }
            if (BoardState[0, 0] == BoardState[1, 1] && BoardState[1, 1] == BoardState[2, 2])
            {
                if (BoardState[0, 2] != 0)
                {
                    GameWinner = BoardState[0, 0];
                    return true;
                }
            }
            if (BoardState[0, 2] == BoardState[1, 1] && BoardState[1, 1] == BoardState[2, 0])
            {
                if (BoardState[0, 2] != 0)
                {
                    GameWinner = BoardState[0, 0];
                    return true;
                }
            }
            return false;
        }

        public bool isValidMove(int move)
        {
            int row = (move - 1) / 3;
            int col = (move - 1) % 3;

            if(IsValidFirsStage())
            {
                if (BoardState[row, col] == 0) return true;
            }
            else if(IsValidSecondStage())
            {
                if(BoardState[row,col] != NextTrun)
                {
                    return false;
                }

                IEnumerable<ConnectedPosition> ConnectedPositions = GetConnectedPosition(row, col);
                IEnumerable<ConnectedPosition> connectectedEmptySports = ConnectedPositions.Where(e => BoardState[e.row, e.col] == 0);
                
                if(connectectedEmptySports.Count() > 0)
                {
                    IEnumerable<ConnectedPosition> connectedPositions = ConnectedPositions.Where(e => BoardState[e.row, e.col] != 0
                    && BoardState[e.row, e.col] != NextTrun); 

                    if(ConnectedPositions.Count() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private IEnumerable<ConnectedPosition> GetConnectedPosition(int row,int col)
        {
            List<ConnectedPosition> connectedPosition = new List<ConnectedPosition>();
            if(row == 0&& col == 0)
            {
                connectedPosition.Add(new ConnectedPosition(0, 1));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(1, 0));
            }
            if (row == 0 && col == 1)
            {
                connectedPosition.Add(new ConnectedPosition(1, 0));
                connectedPosition.Add(new ConnectedPosition(0, 1));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(1, 2));
                connectedPosition.Add(new ConnectedPosition(0, 2));
            }
            if (row == 0 && col == 2)
            {
                connectedPosition.Add(new ConnectedPosition(0, 1));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(1, 2));
            }
            if (row == 1 && col == 0)
            {
                connectedPosition.Add(new ConnectedPosition(0, 0));
                connectedPosition.Add(new ConnectedPosition(0, 1));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(2, 1));
                connectedPosition.Add(new ConnectedPosition(2, 0));
            }
            if (row == 1 && col == 1)
            {
                connectedPosition.Add(new ConnectedPosition(0, 0));
                connectedPosition.Add(new ConnectedPosition(0, 1));
                connectedPosition.Add(new ConnectedPosition(0, 2));
                connectedPosition.Add(new ConnectedPosition(1, 0));
                connectedPosition.Add(new ConnectedPosition(1, 2));
                connectedPosition.Add(new ConnectedPosition(2, 0));
                connectedPosition.Add(new ConnectedPosition(2, 1));
                connectedPosition.Add(new ConnectedPosition(2, 2));
            }

            if (row == 1 && col == 2)
            {
                connectedPosition.Add(new ConnectedPosition(0, 2));
                connectedPosition.Add(new ConnectedPosition(0, 1));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(2, 1));
                connectedPosition.Add(new ConnectedPosition(2, 2));
            }

            if (row == 2 && col == 0)
            {
                connectedPosition.Add(new ConnectedPosition(1,0));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(2, 1));
            }
            if (row == 2 && col == 1)
            {
                connectedPosition.Add(new ConnectedPosition(2, 0));
                connectedPosition.Add(new ConnectedPosition(1, 0));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(1, 2));
                connectedPosition.Add(new ConnectedPosition(2, 2));
            }
            if (row == 2 && col == 2)
            {
                connectedPosition.Add(new ConnectedPosition(2, 1));
                connectedPosition.Add(new ConnectedPosition(1, 1));
                connectedPosition.Add(new ConnectedPosition(1, 2));
            }

            return connectedPosition;
        }
    }
}
