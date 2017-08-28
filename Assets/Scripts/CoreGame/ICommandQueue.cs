using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Commands = CommandQueue.Commands;

public interface ICommandQueue
{
    bool AddCommand(Commands function);
    bool RemoveLastCommand();
    bool ClearCommands();
    bool CallNext(IHasQueue obj);
    bool CallFirst(IHasQueue obj);
    List<Commands> GetCommandQueue();
}
