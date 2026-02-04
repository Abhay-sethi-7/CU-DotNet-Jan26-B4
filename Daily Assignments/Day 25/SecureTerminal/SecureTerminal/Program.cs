namespace SecureTerminal
{
   
    internal class Program  {
        static void Main()
        {
            string pin = "";
            Console.Write("Enter 4-digit PIN: ");

            while (pin.Length < 4)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (char.IsDigit(key.KeyChar))
                {
                    pin += key.KeyChar;
                    Console.Write("*");
                }
               
                else if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                   
                    pin = pin.Substring(0, pin.Length - 1);
                }
              
            }

            Console.WriteLine("Access Code Accepted!");
        }
    }

}
