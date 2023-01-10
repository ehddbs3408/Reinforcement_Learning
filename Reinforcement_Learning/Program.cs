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
		public static GameManager gameManager;

		static void Main(string[] args)
		{
			DPManager = new DynamicProgrammingManager();
			gameManager = new GameManager();

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
			Console.WriteLine("2) 게임 하기");
			Console.WriteLine("3) 프로그램 종료");
			Console.WriteLine(Environment.NewLine);
			Console.Write("동작 선택:");

			switch (Console.ReadLine())
			{
				case "1":
					DPManager.UpdateByDynamicProgramming();
					return true;
				case "2":
					gameManager.PlayGame();
					return true;
				case "3":
					return false;
				default:
					return true;
			}
		}

		
	}
}