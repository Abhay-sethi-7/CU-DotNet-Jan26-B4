namespace LINQLearning
{
    
    class Employee
    {
        public int Id;
        public string Name;
        public string Dept;
        public double Salary;
        public DateTime JoinDate;
    }

    class Querry2
    {
        static void Main()
        {
            var employees = new List<Employee>
        {
            new Employee{Id=1, Name="Ravi", Dept="IT", Salary=80000, JoinDate=new DateTime(2019,1,10)},
            new Employee{Id=2, Name="Anita", Dept="HR", Salary=60000, JoinDate=new DateTime(2021,3,5)},
            new Employee{Id=3, Name="Suresh", Dept="IT", Salary=120000, JoinDate=new DateTime(2018,7,15)},
            new Employee{Id=4, Name="Meena", Dept="Finance", Salary=90000, JoinDate=new DateTime(2022,9,1)}
        };

            //  Highest & Lowest Salary in each Department
            var salaryStats = employees
                .GroupBy(e => e.Dept)
                .Select(g => new
                {
                    Department = g.Key,
                    Highest = g.Max(e => e.Salary),
                    Lowest = g.Min(e => e.Salary)
                });

            Console.WriteLine("Highest & Lowest Salary per Department:");
            foreach (var item in salaryStats)
                Console.WriteLine($"{item.Department} -> High: {item.Highest}, Low: {item.Lowest}");



            //  Count Employees per Department
            var empCount = employees
                .GroupBy(e => e.Dept)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count()
                });

            Console.WriteLine("\nEmployee Count per Department:");
            foreach (var item in empCount)
                Console.WriteLine($"{item.Department} -> {item.Count}");



            // Employees joined after 2020
            var joinedAfter2020 = employees
                .Where(e => e.JoinDate.Year > 2020);

            Console.WriteLine("\nEmployees Joined After 2020:");
            foreach (var e in joinedAfter2020)
                Console.WriteLine($"{e.Name} - {e.JoinDate.ToShortDateString()}");



            //  Anonymous Objects: Name + Annual Salary
            var annualSalary = employees
                .Select(e => new
                {
                    e.Name,
                    AnnualSalary = e.Salary * 12
                });

            Console.WriteLine("\nAnnual Salaries:");
            foreach (var e in annualSalary)
                Console.WriteLine($"{e.Name} -> {e.AnnualSalary}");
        }
    }

}
