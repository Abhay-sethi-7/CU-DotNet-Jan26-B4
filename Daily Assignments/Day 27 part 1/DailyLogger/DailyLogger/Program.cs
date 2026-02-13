
class DailyLogger
{
    static void Main()
    {
        string filePath = "../../../journal.txt";

        Console.WriteLine("=== Daily Reflection Logger ===");
        Console.Write("Write your reflection for today: ");
        string reflection = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            sw.WriteLine("Date: " + DateTime.Now);
            sw.WriteLine("Reflection: " + reflection);
            sw.WriteLine("-----------------------------------");
        }

        Console.WriteLine("Reflection saved successfully!");
    }
}
