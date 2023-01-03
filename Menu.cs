namespace BookMook
{
    [Serializable]
    internal class Menu
    {
        private string Prompt;
        private string[] Options;
        private int SelectedIndex;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        public void DisplayOptions()
        {
            Console.Clear();
            Console.CursorVisible = false;

            Console.WriteLine("\x1b[37;1;4m" + Prompt + "\n");
            Utils.Info("\x1b[37;1;4mUse \u001b[32m▲\u001b[37m and \u001b[32m▼\u001b[37m to navigate and press \u001b[32mEnter/Return\x1b[37m to select:\u001b[24m");

            for (int i = 0; i < Options.Length; i++)
            {
                string current = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "► \u001b[32m";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(prefix + current);
            }

            Console.ResetColor();
        }

        public int Run()
        {
            bool selected = true;
            DisplayOptions();

            while (selected)
            {
                ConsoleKey consoleKey = Console.ReadKey().Key;

                switch (consoleKey)
                {
                    case ConsoleKey.UpArrow:
                        SelectedIndex--;
                        if (SelectedIndex == -1)
                        {
                            SelectedIndex = Options.Length - 1;
                        }

                        DisplayOptions();
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        if (SelectedIndex == Options.Length)
                        {
                            SelectedIndex = 0;
                        }

                        DisplayOptions();
                        break;
                    case ConsoleKey.Enter:
                        selected = false;
                        break;
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            return SelectedIndex;
        }
    }
}
