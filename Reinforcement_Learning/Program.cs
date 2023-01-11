using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforcement_Learning
{
	class Program
	{
		public static DynamicProgrammingManager DPManager;
		public static GameManager GonuGameManager;
		public static SarsaManager sarsaManager;

		static void Main(string[] args)
		{
			DPManager = new DynamicProgrammingManager();
			GonuGameManager = new GameManager();
			sarsaManager = new SarsaManager();

			bool showMenu = true;

			while (showMenu)
			{
				showMenu = MainMenu();
			}
		}
		private static bool MainMenu()
		{
			Console.Clear();
			Console.WriteLine("원하는 동작을 선택하세요:");
			Console.WriteLine(Environment.NewLine);
			Console.WriteLine("1) 동적프로그래밍 진행");
            Console.WriteLine("2) SARSA");
            Console.WriteLine("3) Q-Learnning");
			Console.WriteLine("4) 게임 하기");
			Console.WriteLine("5) 프로그램 종료");
			Console.WriteLine(Environment.NewLine);
			Console.Write("동작 선택:");

			switch (Console.ReadLine())
			{
				case "1":
					DPManager.UpdateByDynamicProgramming();
					return true;
				case "2":
					sarsaManager.UpdateBySarsa();
					return true;
				case "3":
			
					return true;
				case "4":
					GonuGameManager.PlayGame();
					return true;
				case "5":
					return false;
				default:
					return true;
			}
		}
	}
}