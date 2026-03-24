using StudentManagementSystemLayeredArch.Models;
using StudentManagementSystemLayeredArch.Repositories;
using StudentManagementSystemLayeredArch.Services;

namespace StudentManagementSystemLayeredArch.UI
{
    
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Select Storage:");
                Console.WriteLine("1. In-Memory");
                Console.WriteLine("2. JSON File");

                int choice = int.Parse(Console.ReadLine());

                IStudentRepository repo;

                if (choice == 1)
                    repo = new ListStudentRepository();
                else
                    repo = new JsonStudentRepository();

                StudentService service = new StudentService(repo);

                while (true)
                {
                    Console.WriteLine("\n1. Add Student");
                    Console.WriteLine("2. View All");
                    Console.WriteLine("3. Update");
                    Console.WriteLine("4. Delete");
                    Console.WriteLine("5. Exit");

                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.Write("Id: ");
                            int id = int.Parse(Console.ReadLine());

                            Console.Write("Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Grade: ");
                            double grade = double.Parse(Console.ReadLine());

                            service.AddStudent(new Student { Id = id, Name = name, Grade = grade });
                            break;

                        case 2:
                            var students = service.GetStudents();
                            foreach (var s in students)
                            {
                                Console.WriteLine($"{s.Id} - {s.Name} - {s.Grade}");
                            }
                            break;

                        case 3:
                            Console.Write("Id to update: ");
                            int uid = int.Parse(Console.ReadLine());

                            Console.Write("New Name: ");
                            string uname = Console.ReadLine();

                            Console.Write("New Grade: ");
                            double ugrade = double.Parse(Console.ReadLine());

                            service.UpdateStudent(new Student { Id = uid, Name = uname, Grade = ugrade });
                            break;

                        case 4:
                            Console.Write("Id to delete: ");
                            int did = int.Parse(Console.ReadLine());
                            service.DeleteStudent(did);
                            break;

                        case 5:
                            return;
                    }
                }
            }
        
    }
}
