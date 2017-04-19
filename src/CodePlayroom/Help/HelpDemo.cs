using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlayroom.Help
{
    public class HelpDemo : IDemo
    {
        private class ActionDemo : IDemo
        {
            private Action<string[]> _action;
            private Action _onComplete;

            public string Name { get; }

            public ActionDemo(string name, Action<string[]> action)
            {
                Name = name;
                _action = action;
                _onComplete = () => { };
            }

            public ActionDemo(string name, Action<string[]> action, Action onComplete)
            {
                Name = name;
                _action = action;
                _onComplete = onComplete;
            }

            public void OnComplete() => _onComplete();

            public void Run(params string[] args) => _action(args);
        }

        public string Name => "Help Demo";
        public void OnComplete() => Console.WriteLine();

        private readonly Dictionary<string, IDemo> _demos = new Dictionary<string, IDemo>();

        public void Register(string commands, IDemo demo)
        {
            _demos.Add(commands, demo);
        }

        public void Register(string commands, string name, Action<string[]> action)
        {
            _demos.Add(commands, new ActionDemo(name, action));
        }

        public void Register(string commands, string name, Action<string[]> action, Action onComplete)
        {
            _demos.Add(commands, new ActionDemo(name, action, onComplete));
        }

        public IDemo this[string option] => _demos[option];

        public string GetOption(string input) => _demos.Keys.ToList().FirstOrDefault(o => o.Split(',').Any(s => input.StartsWith(s, StringComparison.OrdinalIgnoreCase)));

        public void Run(params string[] args)
        {
            // Display the options so the user doesn't have to waste time asking what to do...
            _demos.Keys.Where(k => args.Length == 0 || args.Any(k.Contains)).ToList().ForEach(op =>
            {
                var commandName = _demos[op]?.Name ?? Constants.CloseText;
                Console.WriteLine($"\t{commandName}\n\t\t{op.Replace(",", "\n\t\t")}");
            });
        }
    }
}
