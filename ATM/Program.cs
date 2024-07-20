namespace ATM
{
    static class Program
    {
        public static List<Option> options;
        static void Main(string[] args)
        {
            options = new List<Option>
            {
                new Option("Check balance", () => WriteTemporaryMessage("Checking balance...")),
                new Option("Withdraw", () =>  WriteTemporaryMessage("How much would you like to withdraw?")),
                new Option("Deposit", () =>  WriteTemporaryMessage("How much would you like to deposit?")),
                new Option("Cancel", () => Environment.Exit(0)),
            };

            int index = 0;

            WriteMenu(options, options[index]);

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.DownArrow && index + 1 < options.Count)
                {
                    index++;
                    WriteMenu(options, options[index]);
                }
                if (keyInfo.Key == ConsoleKey.UpArrow && index - 1 >= 0)
                {
                    index--;
                    WriteMenu(options, options[index]);
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }

        static void WriteTemporaryMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Thread.Sleep(3000);
            WriteMenu(options, options[0]);
        }

        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
        }
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
