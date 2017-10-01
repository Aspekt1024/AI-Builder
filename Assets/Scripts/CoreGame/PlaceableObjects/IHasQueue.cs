using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Commands = CommandQueue.Commands;

public interface IHasQueue : IObjectAttribute
{
    bool AddCommand(Commands function);
    bool StartQueue();
    bool RemoveLastCommand();
    bool ClearCommands();
    bool CallNext();
    bool RestartQueue();
    void QueueComplete();
    List<Commands> GetCommandQueue();
}
