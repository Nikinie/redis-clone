using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class RedisClient
{
    private readonly string _host;
    private readonly int _port;
    private NetworkStream _stream;
    private StreamReader _reader;
    private TcpClient _tcpClient;

    public RedisClient(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public void Connect()
    {
        _tcpClient = new TcpClient(_host, _port);
        _stream = _tcpClient.GetStream();
        _reader = new StreamReader(_stream, Encoding.UTF8);
        Console.WriteLine("Connected to Redis!");
    }

    public void SendCommand(string command)
    {
        byte[] commandBytes = Encoding.UTF8.GetBytes(command);
        _stream.Write(commandBytes, 0, commandBytes.Length);
    }

    public string ReadResponse()
    {
        string response = _reader.ReadLine();

        if (response == null)
        {
            return "(nil)";
        }

        // Check for bulk strings
        if (response.StartsWith("$"))
        {
            Console.WriteLine($"Raw response: {response}");
            int length = int.Parse(response.Substring(1));
            if (length == -1)
                return "(nil)";
            char[] buffer = new char[length];
            _reader.Read(buffer, 0, length);
            _reader.ReadLine(); // Consume trailing \r\n
            return new string(buffer);
        }

        if (response.StartsWith('+'))
        {
            return response.Remove(0,1);
        }

        return response;
    }
}
