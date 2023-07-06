using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStackHanlder
{
    private Queue<Command> _unexecutedCommands;
    private List<Command> _undoStack;
    private List<Command> _redoStack;

    public CommandStackHanlder(int stackCapacity)
    {
        _unexecutedCommands = new Queue<Command>();
        _undoStack = new List<Command>(stackCapacity);
        _redoStack = new List<Command>(stackCapacity);
    }

    public void RegisterCommand(Command command)
    {
        _unexecutedCommands.Enqueue(command);
    }

    public void ExecuteLast()
    {
        Command command = _unexecutedCommands.Dequeue();
        command.Execute();

        AddUndo(command);
    }

    public void Undo()
    {
        if(_undoStack.Count == 0)
        {
            return;
        }

        Command command = _undoStack[_undoStack.Count - 1];
        command.Undo();

        _undoStack.Remove(command);
        AddRedo(command);
    }

    public void ReDo()
    {
        if (_redoStack.Count == 0)
        {
            return;
        }

        Command command = _redoStack[_redoStack.Count - 1];
        command.Execute();

        _redoStack.Remove(command);
        AddUndo(command);
    }
    
    public void AddUndo(Command command)
    {
        if (_undoStack.Count == _undoStack.Capacity)
        {
            _undoStack.RemoveAt(0);
        }

        _undoStack.Add(command);
    }

    private void AddRedo(Command command)
    {
        if (_redoStack.Count == _redoStack.Capacity)
        {
            _redoStack.RemoveAt(0);
        }

        _redoStack.Add(command);
    }
}
