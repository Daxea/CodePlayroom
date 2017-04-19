using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CodePlayroom.Extern
{
    public class MessageBoxDemo : IDemo
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr h, string m, string c, int type);

        public static MessageBoxResult MessageBox(string message, string title, MessageBoxOptions options)
        {
            return (MessageBoxResult)MessageBox(IntPtr.Zero, message, title, (int)options);
        }

        public string Name => "Message Box Demo";

        public void OnComplete() => Console.Clear();

        public void Run(params string[] args)
        {
            Console.Write("Message to Display: ");
            var input = Console.ReadLine();
            var result = MessageBox(input, "Message Display", GetOptionsFromArgs(args));
            Console.WriteLine($"Result: {result}");
        }

        private readonly Type _typeOfOptions = typeof(MessageBoxOptions);

        private MessageBoxOptions GetOptionsFromArgs(string[] args)
        {
            
            var names = Enum.GetNames(_typeOfOptions);
            var options = 0;
            foreach (var arg in args)
            {
                var name = names.FirstOrDefault(n => n.Equals(arg, StringComparison.OrdinalIgnoreCase));
                if (string.IsNullOrEmpty(name))
                    continue;
                options += (int)(MessageBoxOptions)Enum.Parse(_typeOfOptions, name);
            }
            return (MessageBoxOptions)options;
        }
    }
}