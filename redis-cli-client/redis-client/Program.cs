using System;

class Program
{
    static void Main(string[] args)
    {
        // Default values
        string host = "localhost";
        int port = 6379;

        // Parse command-line arguments
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-h" && i + 1 < args.Length)
            {
                host = args[i + 1];
                i++; // Skip the next argument as it's the value for -h
            }
            else if (args[i] == "-p" && i + 1 < args.Length)
            {
                if (int.TryParse(args[i + 1], out int parsedPort))
                {
                    port = parsedPort;
                }
                i++; // Skip the next argument as it's the value for -p
            }
        }

        Console.WriteLine($"Connecting to Redis at {host}:{port}...");

        try
        {
            var client = new RedisClient(host, port);
            client.Connect();

            while (true)
            {
                Console.Write($"\n{host}:{port}> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                string respCommand = RedisCommand.CreateCommand(input);
                if (string.IsNullOrEmpty(respCommand))
                {
                    Console.WriteLine("Invalid command.");
                    continue;
                }

                if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting...");
                    return;
                }

                client.SendCommand(respCommand);
                string response = client.ReadResponse();
                Console.Write(response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
