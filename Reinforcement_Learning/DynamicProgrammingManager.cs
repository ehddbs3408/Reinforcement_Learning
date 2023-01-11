using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforcement_Learning
{
    public class DynamicProgrammingManager
    {
        public Dictionary<int, float> StateValueFunction;
        public float DiscountFactor = 0.9f;

        int num00 = 0;
        int num10 = 0;
        int num11 = 0;
        int num21 = 0;
        int num22 = 0;
        int num32 = 0;
        int num33 = 0;
        int num43 = 0;
        int num44 = 0;

        public DynamicProgrammingManager()
        {
            StateValueFunction = new Dictionary<int, float>();
        }


        public void UpdateByDynamicProgramming()
        {
            InitializeValueFunction();
            ApplyDynamicProgramming();
        }

        public void InitializeValueFunction()
        {
            Console.Clear();
            Console.WriteLine("동적 프로그래밍 시작");
            Console.WriteLine("가치 함수 초기화");

            StateCountReset();
            StateValueFunction.Clear();

            for (int i = 0; i <= GameParameters.StateCount; i++)
            {
                GameState state = new GameState();
                state.PopulateBoard(i);

                if (state.IsValidSecondStage()) // 2단계 게임 보드일 경우
                {
                    StateValueFunction.Add(i * 3 + 1, 0.0f); // 흑돌이 둘 차례인 상태를 가치 함수 엔트리로 추가
                    StateValueFunction.Add(i * 3 + 2, 0.0f); // 백돌이 둘 차례인 상태를 가치 함수 엔트리로 추가

                    if (state.NumberOfBlacks == 4 && state.NumberOfWhites == 4)
                        num44++;
                }
                else if (state.IsValidFirstStage()) // 1단계 게임 보드일 경우
                {
                    StateValueFunction.Add(i * 3 + state.GetFirstStageTurn(), 0.0f);

                    if (state.NumberOfBlacks == 0 && state.NumberOfWhites == 0)
                        num00++;
                    if (state.NumberOfBlacks == 1 && state.NumberOfWhites == 0)
                        num10++;
                    if (state.NumberOfBlacks == 1 && state.NumberOfWhites == 1)
                        num11++;
                    if (state.NumberOfBlacks == 2 && state.NumberOfWhites == 1)
                        num21++;
                    if (state.NumberOfBlacks == 2 && state.NumberOfWhites == 2)
                        num22++;
                    if (state.NumberOfBlacks == 3 && state.NumberOfWhites == 2)
                        num32++;
                    if (state.NumberOfBlacks == 3 && state.NumberOfWhites == 3)
                        num33++;
                    if (state.NumberOfBlacks == 4 && state.NumberOfWhites == 3)
                        num43++;

                }
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("가치 함수 초기화 완료");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Black 0, White 0: {num00}");
            Console.WriteLine($"Black 1, White 0: {num10}");
            Console.WriteLine($"Black 1, White 1: {num11}");
            Console.WriteLine($"Black 2, White 1: {num21}");
            Console.WriteLine($"Black 2, White 2: {num22}");
            Console.WriteLine($"Black 3, White 2: {num32}");
            Console.WriteLine($"Black 3, White 3: {num33}");
            Console.WriteLine($"Black 4, White 3: {num43}");
            Console.WriteLine($"Black 4, White 4: {num44}");
            Console.WriteLine(Environment.NewLine);
            Console.Write("아무 키나 누르세요:");
            Console.ReadLine();
        }


        public void ApplyDynamicProgramming()
        {
            Console.Clear();
            Console.WriteLine("동적 프로그래밍 적용");
            Console.WriteLine(Environment.NewLine);

            int loopCount = 0;
            bool terminateLoop = false;

            while (!terminateLoop)
            {
                // 업데이트되는 가치 함수값을 임시로 저장하기 위한 dictionary
                Dictionary<int, float> nextStateValueFunction = new Dictionary<int, float>();

                // 동적 프로그래밍 각 단계에서 함수값이 업데이트된 크기
                float valueFunctionUpdateAmount = 0.0f;

                // 가치 함수 업데이트 루프
                foreach (KeyValuePair<int, float> valueFunctionEntry in StateValueFunction)
                {
                    // 가치 함수 업데이트 계산
                    float updatedValue = UpdateValueFunction(valueFunctionEntry.Key);
                    // 업데이트 크기
                    float updatedAmount = Math.Abs(valueFunctionEntry.Value - updatedValue);
                    // 가치 함수 업데이트
                    nextStateValueFunction[valueFunctionEntry.Key] = updatedValue;

                    // 루프를 돌면서 함수가 업데이트된 크기를 기록
                    if (updatedAmount > valueFunctionUpdateAmount)
                        valueFunctionUpdateAmount = updatedAmount;
                }

                // 가치 함수값을 임시 저장 가치 함수로 변경
                StateValueFunction = new Dictionary<int, float>(nextStateValueFunction);

                loopCount++;
                Console.WriteLine($"동적 프로그래밍 {loopCount}회 수행, 업데이트 오차 {valueFunctionUpdateAmount}");

                if (valueFunctionUpdateAmount < 0.01f) // 업데이트 크기가 충분히 작으면 루프 종료
                    terminateLoop = true;
            }

            Console.WriteLine(Environment.NewLine);
            Console.Write("아무 키나 누르세요:");
            Console.ReadLine();
        }

        public float UpdateValueFunction(int gameStateKey)
        {
            // 주어진 게임 상태 키에 대해 게임 상태 생성
            GameState gameState = new GameState(gameStateKey);

            if (gameState.isFinalState()) // 게임 종료 상태이면 함수값 0 반환
                return 0.0f;

            List<float> actionExpectationList = new List<float>();

            // 1부터 9까지의 모든 행동에 대해
            for (int i = GameParameters.ActionMinIndex; i <= GameParameters.ActionMaxIndex; i++)
            {
                if (gameState.IsValidMove(i)) // 이 행동이 올바른 행동이면
                {
                    GameState nextState = gameState.GetNextState(i); // 행동을 통해 전이해 간 다음 상태 구성
                    float reward = nextState.GetReward(); // 다음 상태에서의 보상값 확인

                    // 행동 가치 함수값 계산
                    float actionExpectation = reward + DiscountFactor * StateValueFunction[nextState.BoardStateKey];

                    actionExpectationList.Add(actionExpectation); // 계산된 가치 함수값을 저장
                }
            }

            if (actionExpectationList.Count > 0) // 루프 종료 후 반환할 가치 함수값 선택
            {
                if (gameState.NextTurn == 1) // 흑돌이 둘 차례이면 저장된 가치 함수값 중 최대값 반환
                    return actionExpectationList.Max();
                else if (gameState.NextTurn == 2) // 백돌이 둘 차례이면 저장된 가치 함수값 중 최소값 반환
                    return actionExpectationList.Min();
            }
            return 0.0f;
        }



        private void StateCountReset()
        {
            num00 = 0;
            num10 = 0;
            num11 = 0;
            num21 = 0;
            num22 = 0;
            num32 = 0;
            num33 = 0;
            num43 = 0;
            num44 = 0;
        }


        public int GetNextMove(int boardStateKey)
        {
            // 주어진 게임 상태에 대해서 선택할 수 있는 행동 후보값을 구한 후,
            IEnumerable<int> actionCandidates = GetNextMoveCandidate(boardStateKey);
            if (actionCandidates.Count() == 0)
                return 0;

            // 그 중 한 값을 랜덤하게 선택해서 반환
            return actionCandidates.ElementAt(new Random().Next(0, actionCandidates.Count()));
        }


        public IEnumerable<int> GetNextMoveCandidate(int boardStateKey)
        {
            float selectedExpectation = 0.0f;

            // 주어진 상태에 대한 게임 상태 생성
            GameState gameState = new GameState(boardStateKey);
            Dictionary<int, float> actionCandidateDictionary = new Dictionary<int, float>();

            // 1부터 9까지의 모든 행동에 대해
            for (int i = GameParameters.ActionMinIndex; i <= GameParameters.ActionMaxIndex; i++)
            {
                if (gameState.IsValidMove(i)) // 이 행동에 이 상태에 적용 가능한 올바른 행동인 경우
                {
                    GameState nextState = gameState.GetNextState(i); //그 행동을 통해 전이해가는 상태를 구하고
                    float reward = nextState.GetReward(); // 그 전이해 간 상태에서의 보상값

                    // 행동 가치 함수값 계산
                    float actionExpectation = reward + DiscountFactor * StateValueFunction[nextState.BoardStateKey];

                    // 행동과 그 행동에 대한 행동 가치 함수값을 저장
                    actionCandidateDictionary.Add(i, actionExpectation);
                }
            }

            if (actionCandidateDictionary.Count == 0)
                return new List<int>();

            if (gameState.NextTurn == 1) // 흑돌 차례인 경우 저장된 행동 가치 함수값 중 최대값을 선택
            {
                selectedExpectation = actionCandidateDictionary.Select(e => e.Value).Max();
            }
            else if (gameState.NextTurn == 2) // 백돌 차례인 경우 저장된 행동 가치 함수값 중 최소값 선택
            {
                selectedExpectation = actionCandidateDictionary.Select(e => e.Value).Min();
            }

            // 선택한 가치 함수값을 가지는 행동들을 모두 모아서 반환
            return actionCandidateDictionary.Where(e => e.Value == selectedExpectation).Select(e => e.Key);
        }
    }
}