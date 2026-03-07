
using System.Collections.Generic;
namespace StreamBuzz
{
    public class CreatorStats
    {
        public string CreatorName { get; set; }
        public double[] weeklyLikes { get; set; }
    }
    internal class Program
    {
        public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();
        public void RegisterCreator(CreatorStats record)
        {
            if (!EngagementBoard.Contains(record))
            {
                EngagementBoard.Add(record);
            }
            else Console.WriteLine("Creator Already present");
        }
        public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
        {
            Dictionary<string, int> recordAnalysis = new Dictionary<string, int>();
            foreach (var r in records)
            {
                int count = 0;

                foreach (var like in r.weeklyLikes)
                {
                    if (like >= likeThreshold)
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    recordAnalysis[r.CreatorName] = count;
                }


            }
            return recordAnalysis;

        }

        public double CalculateAverageLikes()
        {
            double total = 0;
            int count = 0;

            foreach (var creator in EngagementBoard)
            {
                foreach (var like in creator.weeklyLikes)
                {
                    total += like;
                    count++;
                }
            }

            if (count == 0)
                return 0;

            return total / count;
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            int choice;

            do
            {
                Console.WriteLine("1. Register Creator");
                Console.WriteLine("2. Show Top Posts");
                Console.WriteLine("3. Calculate Average Likes");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreatorStats cs = new CreatorStats();
                        Console.WriteLine("Enter Creator Name:");
                        cs.CreatorName = Console.ReadLine();

                        cs.weeklyLikes = new double[4];
                        Console.WriteLine("Enter weekly likes (Week 1 to 4):");

                        for (int i = 0; i < 4; i++)
                        {
                            cs.weeklyLikes[i] = double.Parse(Console.ReadLine());
                        }

                        p.RegisterCreator(cs);
                        Console.WriteLine("Creator registered successfully");
                        break;

                    case 2:
                        Console.WriteLine("Enter like threshold:");
                        double threshold = double.Parse(Console.ReadLine());

                        var result = p.GetTopPostCounts(EngagementBoard, threshold);

                        if (result.Count == 0)
                        {
                            Console.WriteLine("No top-performing posts this week");
                        }
                        else
                        {
                            foreach (var item in result)
                            {
                                Console.WriteLine(item.Key + " - " + item.Value);
                            }
                        }
                        break;

                    case 3:
                        double avg = p.CalculateAverageLikes();
                        Console.WriteLine("Overall average weekly likes: " + avg);
                        break;

                    case 4:
                        Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                        break;
                }

            } while (choice != 4);
        }
    }
}