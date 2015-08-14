using System;
using Entities;
using Shared;
using Shared.Properties;
//using System.Threading;

namespace ConsoleApplication
{
    class Program
    {
        private const string Yes = "Y";
        private const string No = "N";

        static void Main()
        {
            //InputParser.Instance.InitializeGame(Patterns.FPentomino, OnGenerationCompleted);
            //while (true)
            //{
            //    InputParser.Instance.CurrentGame.ProcessGeneration();
            //    Thread.Sleep(100);
            //    Console.Clear();
            //}
            string anotherGame;
            do
            {
                anotherGame = string.Empty;
                PrepareConsole();
                var option = GetSelectedOption();
                if (option == 5) continue;
                StartGame(option);
                anotherGame = ContinueAnotherGame();
            } while (!string.IsNullOrEmpty(anotherGame)
                     && anotherGame.ToUpper().Equals(Yes));
        }
        
        private static void PrepareConsole()
        {
            Console.Clear();
            Console.WriteLine(Resources.Title);
            Console.WriteLine(Resources.Underline);
            Console.WriteLine();
            Console.WriteLine(Resources.Option1);
            Console.WriteLine(Resources.Option2);
            Console.WriteLine(Resources.Option3);
            Console.WriteLine(Resources.Option4);
            Console.WriteLine(Resources.Option5);
            Console.WriteLine();
        }

        private static int GetSelectedOption()
        {
            int option;
            do
            {
                Console.Write(Resources.SelectOption);
                var input = Console.ReadLine();
                int.TryParse(input, out option);
            } while (option <= 0 || option > 5);
            return option;
        }

        private static void StartGame(int option)
        {
            var pattern = GetSelectedPattern(option);
            InputParser.Instance.InitializeGame(pattern, OnGenerationCompleted);
            PrintInput(pattern);
            InputParser.Instance.ProcessFirstGeneration();
        }
        
        private static string GetSelectedPattern(int option)
        {
            var pattern = string.Empty;
            switch (option)
            {
                case 1:
                    pattern = Patterns.Block;
                    break;
                case 2:
                    pattern = Patterns.Boat;
                    break;
                case 3:
                    pattern = Patterns.Blinker1;
                    break;
                case 4:
                    pattern = Patterns.Toad1;
                    break;
                case 5:
                    break;
            }
            return pattern;
        }

        private static void PrintInput(string pattern)
        {
            Console.WriteLine();
            Console.WriteLine(Resources.Input);
            Console.WriteLine(pattern);
            Console.WriteLine();
        }

        private static string ContinueAnotherGame()
        {
            string anotherGame;
            do
            {
                Console.Write(Resources.ContinueMessage);
                anotherGame = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(anotherGame)
                     || !(anotherGame.ToUpper().Equals(Yes) || anotherGame.ToUpper().Equals(No)));
            return anotherGame;
        }
        
        private static void OnGenerationCompleted(int generation, IBoard nextgeneration)
        {
            Console.WriteLine(Resources.Output);
            Console.WriteLine(nextgeneration.ToString());
            Console.WriteLine();
        }
    }
}