namespace TheWorld.Display;

public static class ConsoleExtensions
{
    extension(Console)
    {
        public static ConsoleColor WarningColor => ConsoleColor.Yellow;
        public static ConsoleColor ErrorColor => ConsoleColor.Red;
        public static ConsoleColor SuccessColor => ConsoleColor.Green;
        public static ConsoleColor InfoColor => ConsoleColor.Cyan;

        public static void Warn(string format, params object[] args)
        {
            Console.ForegroundColor = Console.WarningColor;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public static void Error(string format, params object[] args)
        {
            Console.ForegroundColor = Console.ErrorColor;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public static void Success(string format, params object[] args)
        {
            Console.ForegroundColor = Console.SuccessColor;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public static void Info(string format, params object[] args)
        {
            Console.ForegroundColor = Console.InfoColor;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }
    }
}