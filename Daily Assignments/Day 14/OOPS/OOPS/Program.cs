namespace OOPS
{
    class Employee
    {
        private int id;

        public void SetId(int id)
        {
            this.id = id;
        }
        public int GetId()
        {
            return id;
        }
        public string Name { get; set; }
        private string department;

        public string Department
        {
            get { return department; }
            set
            {
                if (value == "Accounts" || value == "Sales" || value == "IT")
                {
                    department = value;
                }
                else
                {
                    Console.WriteLine("Invalid Department! Allowed: Accounts, Sales, IT");
                }
            }
        }

        private int salary;

        public int Salary
        {
            get { return salary; }
            set
            {
                if (value >= 50000 && value <= 90000)
                {
                    salary = value;
                }
                else
                {
                    Console.WriteLine("Invalid Salary! Range: 50000 to 90000");
                }
            }
        }


        public void Display()
        {
            Console.WriteLine("Employee Details");
            Console.WriteLine($"ID         : {id}");
            Console.WriteLine($"Name       : {Name}");
            Console.WriteLine($"Department : {department}");
            Console.WriteLine($"Salary     : {salary}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee();

            emp.SetId(101);
            emp.Name = "Abhay";
            emp.Department = "IT";
            emp.Salary = 65000;

            emp.Display();
        }
    }
}