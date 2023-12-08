namespace Lab2;

public static class Logger
{
    public static void Log( string message, params object?[] args )
    {
        Console.WriteLine( message, args );
    }
}