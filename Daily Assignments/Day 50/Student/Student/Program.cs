namespace Student
{
    
      public class Students
        {
            public string SName { get; set; }
            public int SID { get; set; }

            public override string ToString()
            {
                return $"Name: {SName} ID: {SID}";
            }
            public override bool Equals(object? obj)
            {
                if (obj is Students other)
                    return this.SID == other.SID && this.SName == other.SName;

                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(SID, SName);
            }

        }

        public class Management
        {
            private Dictionary<Students, int> student = new Dictionary<Students, int>();

            public void AddOrUpdateStudent(Students s, int marks)
            {
                if (student.TryAdd(s, marks))
                {
                    Console.WriteLine($"Student with id added with marks: " + marks);
                }
                else
                {
                    Console.WriteLine("Student already exists.");

                    if (marks > student[s])
                    {
                        student[s] = marks;
                        Console.WriteLine("Marks updated to: " + marks);
                    }
                    else
                    {
                        Console.WriteLine("Marks not updated. Existing marks are higher or equal.");
                    }
                }
            }

            public void Display()
            {
                foreach (var s in student)
                {
                    Console.WriteLine($"{s.Key} and marks: {s.Value}");
                }
            }
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                Management m = new Management();

                Students s1 = new Students { SName = "A", SID = 9 };
                Students s2 = new Students { SName = "B", SID = 2 };
                Students s3 = new Students { SName = "C", SID = 3 };
                Students s4 = new Students { SName = "D", SID = 4 };
                Students s5 = new Students { SName = "D", SID = 4 };
                m.AddOrUpdateStudent(s1, 90);
                m.AddOrUpdateStudent(s1, 100);
                m.AddOrUpdateStudent(s2, 80);
                m.AddOrUpdateStudent(s2, 40);
                m.AddOrUpdateStudent(s4, 30);
                m.AddOrUpdateStudent(s5, 50);
                m.AddOrUpdateStudent(new Students { SName = "C", SID = 3 }, 38);
                m.Display();

            }
        }
    }


