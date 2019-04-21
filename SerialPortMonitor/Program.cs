using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// portname baudrate
// portname baudrate parity databits stopbits handshake

class Program
{
    public const int RETURN_INCORRECT_ARG = 1;
    public const int RETURN_OK = 0;
    static int Main(string[] args)
    {
        SerialPortMonitor serialPortMonitor;
        if (args.Length != 2 && args.Length != 6)
        {
            Console.WriteLine("Incorrect number of arguments specified. Please use the following formula:\n\tspm.exe portname baudrate\nor\n\tspm.exe portname baudrate parity databits stopbits handshake");
            Console.WriteLine("Gave {0} arguments", args.Length);
            return RETURN_INCORRECT_ARG;
        }
        int baudrate;
        if(!int.TryParse(args[1], out baudrate))
        {
            Console.WriteLine("Baudrate argument could not be parsed.");
            return RETURN_INCORRECT_ARG;
        }
        CommandInterpreter interpreter = new CommandInterpreter();
        interpreter.AddFunctionHandler(Commands.CMD_PLAY_NARRATION)
        serialPortMonitor = new SerialPortMonitor(args[0], baudrate);
        serialPortMonitor.AddToOnReadEvent(interpreter.Interpret);




        return RETURN_OK;
    }
}

