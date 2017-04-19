using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodePlayroom.Commands
{
    public class PrintCommand : ICommand
    {
        private readonly string _text;

        public PrintCommand(string text)
        {
            _text = text;
        }

        public IEnumerator Run()
        {
            Thread.Sleep(2500);
            Console.WriteLine(_text);
            yield return null;
        }
    }
}