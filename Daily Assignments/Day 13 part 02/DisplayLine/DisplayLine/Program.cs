namespace DisplayLine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayLine();
            DisplayLine('+');
            DisplayLine('$', 60);

        }
        static void DisplayLine(char ch = '-', int count = 40)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(ch);
            }
            Console.WriteLine();
        }

    }
}
