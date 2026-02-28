
namespace CSVDemo
{
    public class Player
    {
        public string Name { get; set; }
        public int RunsScored { get; set; }
        public int BallsFaced { get; set; }
        public bool IsOut { get; set; }
        public double StrikeRate { get; set; }
        public double Average { get; set; }

        public void CalculateStats()
        {
            StrikeRate = BallsFaced > 0
                ? (double)RunsScored / BallsFaced * 100 : 0;

            Average = IsOut
                ? (double)RunsScored / 1: RunsScored;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../PerformanceReport.CSV";

           
            if (!File.Exists(filePath))
            {
                string[] sampleData =
                {
                    "Steve Smith,84,90,True",
                    "Virat Kohli,29,35,False",
                    "Joe Root,110,120,True",
                    "David Warner,5,4,True",      
                    "Babar Azam,72,65,False"
                };

                File.WriteAllLines(filePath, sampleData);
                Console.WriteLine("CSV file created successfully.\n");
            }

            List<Player> players = new List<Player>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    try
                    {
                        string[] data = line.Split(',');

                        if (!int.TryParse(data[1].Trim(), out int runs) ||
                            !int.TryParse(data[2].Trim(), out int balls) ||
                            !bool.TryParse(data[3].Trim(), out bool isOut))
                        {
                            Console.WriteLine("Invalid data in line: " + line);
                            continue;
                        }

                        Player player = new Player
                        {
                            Name = data[0].Trim(),
                            RunsScored = runs,
                            BallsFaced = balls,
                            IsOut = isOut
                        };

                        player.CalculateStats();

                        if (player.BallsFaced >= 10)
                            players.Add(player);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Invalid format in line: " + line);
                    }
                }

                var sortedPlayers = players
                    .OrderByDescending(p => p.StrikeRate)
                    .ToList();

                Console.WriteLine("{0,-20} {1,5} {2,10} {3,10}",
                                  "Name", "Runs", "SR", "Avg");
                Console.WriteLine("------------------------------------------------------------");

                foreach (var p in sortedPlayers)
                {
                    Console.WriteLine("{0,-20} {1,5} {2,10:F2} {3,10:F2}",
                                      p.Name, p.RunsScored, p.StrikeRate, p.Average);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }

            Console.ReadLine();
        }
    }
}