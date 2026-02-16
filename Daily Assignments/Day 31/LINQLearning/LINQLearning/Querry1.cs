

namespace LINQLearning
{
    class Student
    {
        public int Id;
        public string Name;
        public string Class;
        public int Marks;
    }

    class Querry1
    {
        static void Main()
        {
            var students = new List<Student>
            {
                new Student{Id=1, Name="Amit", Class="10A", Marks=85},
                new Student{Id=2, Name="Neha", Class="10A", Marks=72},
                new Student{Id=3, Name="Rahul", Class="10B", Marks=90},
                new Student{Id=4, Name="Pooja", Class="10B", Marks=60},
                new Student{Id=5, Name="Kiran", Class="10A", Marks=95}
            };

            // Top 3 Students by Marks
            var top3 = students
                        .OrderByDescending(s => s.Marks)
                        .Take(3);

            Console.WriteLine("Top 3 Students:");
            foreach (var s in top3)
                Console.WriteLine($"{s.Name} - {s.Marks}");

            // Group by Class & Average Marks
            var avgByClass = students
                            .GroupBy(s => s.Class)
                            .Select(g => new
                            {
                                Class = g.Key,
                                Average = g.Average(s => s.Marks)
                            });

            Console.WriteLine("\nAverage Marks By Class:");
            foreach (var c in avgByClass)
                Console.WriteLine($"{c.Class} : {c.Average}");

            // Students Below Class Average
            var belowAvg = students
                            .GroupBy(s => s.Class)
                            .SelectMany(g =>
                            {
                                double avg = g.Average(s => s.Marks);
                                return g.Where(s => s.Marks < avg);
                            });

            Console.WriteLine("\nStudents Below Class Average:");
            foreach (var s in belowAvg)
                Console.WriteLine($"{s.Name} - {s.Class} - {s.Marks}");

            // Order by Class then Marks Descending
            var ordered = students
                            .OrderBy(s => s.Class)
                            .ThenByDescending(s => s.Marks);

            Console.WriteLine("\nOrdered Students:");
            foreach (var s in ordered)
                Console.WriteLine($"{s.Class} - {s.Name} - {s.Marks}");
        }
    }
}
