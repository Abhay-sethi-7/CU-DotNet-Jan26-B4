
namespace HighScoreLeaderboard
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SortedDictionary<double, string> leaderboard = new SortedDictionary<double, string>();

            leaderboard.Add(55.42, "SwiftRacer");
            leaderboard.Add(52.10, "SpeedDemon");
            leaderboard.Add(58.91, "SteadyEddie");
            leaderboard.Add(51.05, "TurboTom");

            Console.WriteLine("Leaderboard (Fastest First):");
            foreach (KeyValuePair<double, string> entry in leaderboard)
            {
                Console.WriteLine(entry.Value + " - " + entry.Key + " sec");
            }

            double fastestTime = 0;
            string fastestPlayer = "";

            foreach (KeyValuePair<double, string> entry in leaderboard)
            {
                fastestTime = entry.Key;
                fastestPlayer = entry.Value;
                break; 
            }

            Console.WriteLine($"Gold Medal: {fastestPlayer}  with  {fastestTime}  sec");

            Console.WriteLine(" Updating SteadyEddie's lap time...");

            leaderboard.Remove(58.91);         
            leaderboard.Add(54.00, "SteadyEddie"); 
            Console.WriteLine("Updated Leaderboard:");
            foreach (KeyValuePair<double, string> entry in leaderboard)
            {
                Console.WriteLine($"{entry.Value} - { entry.Key} sec");
            }

            Console.ReadLine();
        }
    }
}
