using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodePlayroom.Commands
{
    public class CommandsDemo : IDemo
    {
        public string Name => "Commands Demo";
        public void OnComplete() => Console.Clear();

        private readonly Random _random;

        public CommandsDemo()
        {
            _random = new Random();
        }

        public CommandsDemo(int seed)
        {
            _random = new Random(seed);
        }

        public void Run(params string[] args)
        {
            var printHelloWorld = new PrintCommand("Hello, World!");
            var sumCommand = new SumCommand(GetRandomNumbers(5, -1000, 1000));

            var isDone = false;
            CommandProcessor.ProcessCommandsAsync(() => { isDone = true; }, printHelloWorld, sumCommand);

            while (!isDone)
            {
                Console.WriteLine("Processing commands, please wait...");
                Thread.Sleep(1000);
            }
        }

        private int[] GetRandomNumbers(int amount, int min, int max)
        {
            var numbers = new int[amount];
            for (var i = 0; i < amount; i++)
                numbers[i] = _random.Next(min, max);
            return numbers;
        }
    }
}