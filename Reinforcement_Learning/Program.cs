using System;

namespace Reinforcement_Learning
{
    
    class Program
    {
        public static DynamicProgrammingManager DPManager;
        static void Main(string[] args)
        {
            DPManager = new DynamicProgrammingManager();
            bool showMenu = true;

            while(showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("동작번호를 입력하세요");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("1)동적프로그래밍 실행");
            Console.WriteLine("2)개암허기");
            Console.WriteLine("3)나가기");
            Console.WriteLine(Environment.NewLine);
            Console.Write("동적 선택 : ");
   
            switch(Console.ReadLine())
            {
                case "1":
                    DPManager.UpdateByDynamicProgramming();
                    return true;
                case "2":
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }
    }
}
