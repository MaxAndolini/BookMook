namespace BookMook
{
    internal class Utils
    {
        public static void Question(string message)
        {
            Console.WriteLine("\x1b[36;1m[?] \u001b[37;24m" + message);
        }

        public static void Info(string message)
        {
            Console.WriteLine("\x1b[32;1m[*] \u001b[37;24m" + message);
        }

        public static void Error(string message)
        {
            Console.WriteLine("\x1b[31;1m[!] \u001b[37;24m" + message);
        }
    }
}
