using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class CommandInterpreter
{
    public delegate void CommandDelegate(string arg);
    Dictionary<string, CommandDelegate> functionSet;
    StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
    public CommandInterpreter()
    {
        functionSet = new Dictionary<string, CommandDelegate>();
    }
    public bool Interpret(string command)
    {
        Console.WriteLine("Parsing " + command);
        string[] splitCommand = command.Split(new char[] { ' ', '\r' });
        Dictionary<string, CommandDelegate>.Enumerator enumerator = functionSet.GetEnumerator();
        while(stringComparer.Compare(enumerator.Current.Key, splitCommand[0]) != 0)
        {
            if (!enumerator.MoveNext())
                return false;
        }

        if (splitCommand.Length>1)
            enumerator.Current.Value(splitCommand[1]);
        else
            enumerator.Current.Value("");
        return true;
    }
    public bool AddFunctionHandler(string command, CommandDelegate handler)
    {
        if(functionSet.ContainsKey(command))
        {
            CommandDelegate currentHandler;
            functionSet.TryGetValue(command, out currentHandler);
            currentHandler += handler;
            return false;
        }
        functionSet.Add(command, handler);
        return true;
    }

}

