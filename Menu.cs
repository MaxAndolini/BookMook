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
            Utils.Info("\x1b[37;1;4mUse \x1b[32m▲\x1b[37m and \x1b[32m▼\x1b[37m to navigate and press \x1b[32mEnter/Return\x1b[37m to select:\x1b[24;0m");

            for (int i = 0; i < Options.Length; i++)
            {
                string current = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "\u001b[30;47m► \x1b[32m" + current;
                }
                else
                {
                    prefix = "\x1b[37;40m  " + current;
                }

                Console.WriteLine(prefix + "\x1b[0m");
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
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

            return SelectedIndex;
        }
    }
}
