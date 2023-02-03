using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public static CommandManager Instance { get; private set; } //This static CommandManager Instance will allow access to an instance of this script and will allow you to create new commands, and call add command with new command to execute the command and add to stack 

    private Stack<ICommand> m_CommandsBuffer = new Stack<ICommand>(); //Will allow for commands to be pushed to this stack and allows for top element to be popped off for implementing undo functionality

    private void Awake()
    {
        Instance = this; 
    }

    public void AddCommand(ICommand command)
    {
        command.Execute(); //Execute the desired command
        m_CommandsBuffer.Push(command); //Push the command to the stack
    }

    public void Undo()
    {
        if (m_CommandsBuffer.Count == 0) //If there are no commands in the stack then return
            return;
        var cmd = m_CommandsBuffer.Pop(); //Create a new command that is intialized as the command that is on top of the stack
        cmd.Undo(); //Call the undo function of the command popped off the top of the stack
    }
}
