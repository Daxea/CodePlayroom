using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodePlayroom.Commands
{
    public class SumCommand : ICommand
    {
        private readonly int[] _numbers;

        public SumCommand(params int[] numbers)
        {
            _numbers = numbers;
        }

        public IEnumerator Run()
        {
            var sum = 0;

            foreach (var number in _numbers)
            {
                sum += number;
                Thread.Sleep(1000);
                yield return null;
            }

            Console.WriteLine($"Sum: {sum}");
        }
    }
}