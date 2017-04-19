using System;
using System.Collections;

namespace CodePlayroom.Commands
{
    public interface ICommand
    {
        IEnumerator Run();
    }
}