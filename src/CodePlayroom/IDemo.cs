using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlayroom
{
    public interface IDemo
    {
        string Name { get; }
        void OnComplete();

        void Run(params string[] args);
    }
}