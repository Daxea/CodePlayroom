using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlayroom.Commands
{
    public class CommandProcessor
    {
        public async Task Process(ICommand command)
        {
            await Task.Run(() =>
            {
                var run = command.Run();
                while (run.MoveNext()) { }
            });
        }

        public static async void ProcessCommandsAsync(params ICommand[] commands)
        {
            var processor = new CommandProcessor();
            foreach (var command in commands)
                await processor.Process(command);
        }

        public static async void ProcessCommandsAsync(Action postProcessorAction, params ICommand[] commands)
        {
            var processor = new CommandProcessor();
            foreach (var command in commands)
                await processor.Process(command);
            postProcessorAction();
        }
    }
}