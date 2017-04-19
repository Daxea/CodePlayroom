using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace CodePlayroom.Reflection
{
    public class TypeCollectionDemo : IDemo
    {
        public string Name => "Type Collection Demo";
        public void OnComplete() => Thread.Sleep(100);

        public void Run(params string[] args)
        {
            var typeCollection = TypeCollection.GetAllTypesDerivedFrom<IDemo>(typeof(IDemo).GetTypeInfo().Assembly);
            Console.WriteLine($"Types found: {typeCollection.Count}\n----------------");
            foreach (var type in typeCollection)
            {
                Console.WriteLine(type.Name);
            }
        }
    }
}