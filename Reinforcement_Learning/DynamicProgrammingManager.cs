﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforcement_Learning
{
    class DynamicProgrammingManager
    {
        public Dictionary<int, float> StateValueFuction;
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
            StateValueFuction = new Dictionary<int, float>();
        }

        public void UpdateByDynamicProgramming()
        {
            InitializeValueFuction();
        }

        public void InitializeValueFuction()
        {
            Console.Clear();
            Console.WriteLine("동적프로그래밍 시작");
            Console.WriteLine("가치 함수 초기화");

            StateCountReset();
            StateValueFuction.Clear();

            for(int i = 0;i<GameParameters.SteteCount;i++)
            {
                GameState state = new GameState();
                state.PopulateBoard(i);
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("가치 함수 초기화 완료");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Black 0, White 0 : {num00}");
            Console.WriteLine($"Black 1, White 0 : {num10}");
            Console.WriteLine($"Black 1, White 1 : {num11}");
            Console.WriteLine($"Black 2, White 1 : {num21}");
            Console.WriteLine($"Black 2, White 2 : {num22}");
            Console.WriteLine($"Black 3, White 2 : {num32}");
            Console.WriteLine($"Black 3, White 3 : {num33}");
            Console.WriteLine($"Black 4, White 3 : {num43}");
            Console.WriteLine($"Black 4, White 4 : {num44}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("아무 키나 누르세요");
            Console.ReadLine();

        }

        public void ApplyDynamicProgramming()
        {

        }

        public void StateCountReset()
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
    }
}