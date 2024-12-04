using System.Text;

public static class RedisCommand
{
    // Parse the command and generate the corresponding RESP format
    public static string CreateCommand(string input)
    {
        string[] parts = input.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return string.Empty;
        string command = parts[0].ToUpper();
        string[] args = parts.Skip(1).ToArray();
        return ConvertToRESP(command, args);
    }

    // Convert a command and its arguments to RESP format
    private static string ConvertToRESP(string command, string[] args)
    {
        StringBuilder respCommand = new StringBuilder($"*{args.Length + 1}\r\n");
        respCommand.Append($"${command.Length}\r\n{command}\r\n");
        foreach (var arg in args)
        {
            respCommand.Append($"${arg.Length}\r\n{arg}\r\n");
        }
        return respCommand.ToString();
    }
}
