using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// The default is 8 data bits, no parity, one stop bit.
// portname baudrate timeout
// portname baudrate parity databits stopbits handshake timeout
class SerialPortMonitor
{
    public const int DEFAULT_CONFIG_BAUDRATE = 9600;
    public const Parity DEFAULT_CONFIG_PARITY = Parity.None;
    public const int DEFAULT_CONFIG_DATABITS = 8;
    public const StopBits DEFAULT_CONFIG_STOPBITS = StopBits.One;
    public const Handshake DEFAULT_CONFIG_HANDSHAKE = Handshake.None;
    public const int DEFAULT_CONFIG_TIMEOUT = 500;
    public const int RETURN_INCORRECT_ARG = 1;
    public const int RETURN_OK = 0;
    public delegate bool OnLineReadDelegate(string cmd);
    OnLineReadDelegate OnLineRead;
    bool isRunning;
    SerialPort serialPort;
    Thread reader;
    string msg;
    StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
    public SerialPortMonitor(string portname, int baudrate) : this(portname, baudrate, DEFAULT_CONFIG_PARITY, DEFAULT_CONFIG_DATABITS, DEFAULT_CONFIG_STOPBITS, DEFAULT_CONFIG_TIMEOUT)
    {
    }
    public SerialPortMonitor(string portname, int baudrate, int timeout) : this(portname, baudrate, DEFAULT_CONFIG_PARITY, DEFAULT_CONFIG_DATABITS, DEFAULT_CONFIG_STOPBITS, timeout)
    {
    }
    public SerialPortMonitor(string portname, int baudrate, Parity parity, int databits, StopBits stopbits, int timeout)
    {
        serialPort = new SerialPort(portname, baudrate, parity, databits, stopbits);
        reader = new Thread(ReaderRead);
        serialPort.ReadTimeout = timeout;
        serialPort.WriteTimeout = timeout;
        serialPort.Open();
        isRunning = true;
        reader.Start();
        Console.WriteLine("SerialPortMonitor running.");
        
    }
    private void ReaderRead()
    {
        while (isRunning)
        {
            try
            {
                string msg = serialPort.ReadLine();
                OnLineRead(msg);
                Console.WriteLine(msg);
            }
            catch (TimeoutException) { }
        }
    }
    public void AddToOnReadEvent(OnLineReadDelegate del)
    {
        OnLineRead += del;
    }
    public void ClearEvent()
    {
        OnLineRead = null;
    }
    public void Kill()
    {
        isRunning = false;
        reader.Join();
        serialPort.Close();
    }
    public void Write(string cmd)
    {
        serialPort.Write(cmd+"\n");
    }

}