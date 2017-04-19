using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodePlayroom.Commands;
using CodePlayroom.Extern;
using CodePlayroom.FSM;
using CodePlayroom.Help;
using CodePlayroom.Reflection;

using static System.Console;

namespace CodePlayroom
{
    public class Program
    {
        private static bool _closeProgram = false;

        public static void Main()
        {
            var helpDemo = new HelpDemo();
            BuildDemos(helpDemo);

            while (true)
            {
                WriteLine(Constants.TitleText);
                WriteLine();
                // Find out what the user wants to see...
                Write(Constants.InputMarker);
                var input = ReadLine();
                // Get the first option that contains the option command the user entered.
                var option = helpDemo.GetOption(input);
                // If no option was selected, let them try again.
                if (string.IsNullOrEmpty(option) || helpDemo[option] == null)
                    continue;
                // Get any arguments that may have been passed by the user.
                var args = ParseArgs(option, input);
                // Run the demo
                helpDemo[option].Run(args);
                helpDemo[option].OnComplete();
                // If the closeProgram flag is true, they chose to end it.
                if (_closeProgram)
                    break;
            }

            Write(Constants.ContinueText);
            ReadKey();
        }

        private static string[] ParseArgs(string option, string input)
        {
            var command = option.Split(',').FirstOrDefault(o => input.StartsWith(o, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(command))
                input = input.Replace(command, string.Empty);
            return input.Split(new [] { " -" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static void BuildDemos(HelpDemo help)
        {
            help.Register("help,?", help);
            help.Register("clear,cls,erase all,forget i said anything", "Clear Console", (args) => Clear());
            help.Register("quit,q,exit,end,close application,close console,disengage", "Exit the Program", (args) => _closeProgram = true);
            help.Register("demo commands,run demo --commands", new CommandsDemo());
            help.Register("demo types,run demo --types", new TypeCollectionDemo());
            help.Register("demo fsm,run demo --fsm", new FiniteStateMachineDemo());
            help.Register("demo message,demo mb,run demo --message", new MessageBoxDemo());
        }
    }
}