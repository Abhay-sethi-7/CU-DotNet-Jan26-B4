namespace StudentDB
{
    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int[] Marks { get; set; }
        public override string ToString()
        {
            return $"Id-{ID}  Name-{Name}  Marks-[{string.Join(", ", Marks)}]";
        }

    }
    class StudentManager
    {
        Dictionary<int, Student> studentData = new Dictionary<int, Student>();
        public bool AddStudent(Student student)
        {
            if (!studentData.ContainsKey(student.ID))
            {
                studentData[student.ID] = student;
                return true;
            }
            return false;

        }
        public void DisplayAllStudents()
        {
            if (studentData.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }
            foreach (var student in studentData.Values)
            {
                Console.WriteLine(student);
            }
        }

        public bool UpdateStudent(int id, int[] newMarks)
        {
            Student foundStudent = SearchStudent(id);
            if(foundStudent != null)
            {
                foundStudent.Marks = newMarks;

                return true;
            }return false;

        }
        public void DeleteStudent(int ID)
        {
            if (studentData.ContainsKey(ID))
            {
                Console.WriteLine($"{ID}-{studentData[ID].Name} is removed");
                studentData.Remove(ID);
                
            }
            else
            {
                Console.WriteLine($"student with {ID} not found!!");
            }
        }

        public Student SearchStudent(int id)
        {
            studentData.TryGetValue(id, out Student student);
            return student;
        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            int choice;
            do
            {
                Console.WriteLine("\n===== Student Management Menu =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Student Marks");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter marks: ");
                        

                        int[] marks = new int[5];
                        for (int i = 0; i < 5; i++)
                        {
                            Console.Write($"Enter mark {i + 1}: ");
                            marks[i] = int.Parse(Console.ReadLine());
                        }
                        bool added = manager.AddStudent(new Student
                        {
                            ID = id,
                            Name = name,
                            Marks = marks
                        });

                        Console.WriteLine(added ? "Student added successfully" : "Student ID already exists");
                        break;

                    case 2:
                        manager.DisplayAllStudents();
                        break;

                    case 3:
                        Console.Write("Enter Student ID to search: ");
                        int searchId = int.Parse(Console.ReadLine());
                        Student student = manager.SearchStudent(searchId);

                        if (student == null)
                            Console.WriteLine("Student not found");
                        else
                            Console.WriteLine(student);
                        break;
                    case 4:
                        Console.Write("Enter Student ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());

                        Console.Write("Enter  new marks: ");
                        

                        int[] newMarks = new int[5];
                        for (int i = 0; i < 5; i++)
                        {
                            Console.Write($"Enter mark {i + 1}: ");
                            newMarks[i] = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine(
                               manager.UpdateStudent(updateId, newMarks)
                               ? "Student updated successfully"
                               : "Student not found"
                           );
                        break;

                    case 5:
                        Console.Write("Enter Student ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        manager.DeleteStudent(deleteId);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

            } while (choice != 0);



















            manager.AddStudent(new Student()
            {
                ID = 111,
                Name = "x",
                Marks = new int[] { 1, 4, 5, 6, 7 }

            });
            manager.AddStudent(new Student()
            {
                ID = 131,
                Name = "y",
                Marks = new int[] { 44, 1, 24, 4, 6 }

            });
            int searchID = 111;
            Student foundStudent = manager.SearchStudent(searchID);
            if (foundStudent == null)
            {
                Console.WriteLine($"{searchID} not  found");
                
            }
            else Console.WriteLine(foundStudent);
            Console.WriteLine("--------------------");
            manager.DeleteStudent(searchID);
            manager.DisplayAllStudents();
            bool updated = manager.UpdateStudent(131, new int[] { 2, 4, 5, 6 });
            if (updated)
            {
                Console.WriteLine("Student updated successfully");
                manager.DisplayAllStudents();
            }

            else
                Console.WriteLine("Student not found");

        }
    }
}
