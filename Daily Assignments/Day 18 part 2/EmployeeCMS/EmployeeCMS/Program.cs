

namespace EmployeeCMS
{
    class Employee
    {

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public int ExperienceInYears { get; set; }


        public Employee(int id, string name, decimal salary, int experience)
        {
            EmployeeId = id;
            EmployeeName = name;
            BasicSalary = salary;
            ExperienceInYears = experience;
        }


        public decimal CalculateAnnualSalary()
        {
            return BasicSalary * 12;
        }


        public void DisplayEmployeeDetails()
        {
            Console.WriteLine("Employee ID: " + EmployeeId);
            Console.WriteLine("Employee Name: " + EmployeeName);
            Console.WriteLine("Basic Salary: " + BasicSalary);
            Console.WriteLine("Experience (Years): " + ExperienceInYears);
            Console.WriteLine("Annual Salary: " + CalculateAnnualSalary());
        }
    }
    class PermanentEmployee : Employee
    {

        public PermanentEmployee(int id, string name, decimal salary, int experience)
            : base(id, name, salary, experience)
        {
        }


        public new decimal CalculateAnnualSalary()
        {
            decimal hra = BasicSalary * 0.20m;
            decimal specialAllowance = BasicSalary * 0.10m;
            decimal loyaltyBonus = 0;

            if (ExperienceInYears >= 5)
            {
                loyaltyBonus = 50000;
            }

            decimal annualSalary =
                (BasicSalary + hra + specialAllowance) * 12 + loyaltyBonus;

            return annualSalary;
        }
    }
    class ContractEmployee : Employee
    {

        public int ContractDurationInMonths { get; set; }

        public ContractEmployee(
            int id,
            string name,
            decimal salary,
            int experience,
            int contractDuration)
            : base(id, name, salary, experience)
        {
            ContractDurationInMonths = contractDuration;
        }

        public new decimal CalculateAnnualSalary()
        {
            decimal contractBonus = 0;

            if (ContractDurationInMonths >= 12)
            {
                contractBonus = 30000;
            }

            decimal annualSalary = (BasicSalary * 12) + contractBonus;
            return annualSalary;
        }
    }
    class InternEmployee : Employee
    {
        public InternEmployee(int id, string name, decimal salary, int experience)
            : base(id, name, salary, experience)
        {
        }

        public new decimal CalculateAnnualSalary()
        {
            return BasicSalary * 12;
        }
    }

    internal class Program
    {
        static void Main()
        {

            Employee e1 = new Employee(1, "A", 10000, 1);

            Employee e2 = new PermanentEmployee(2, "B", 20000, 6);
            PermanentEmployee e3 = new PermanentEmployee(3, "C", 20000, 6);

            Employee e4 = new ContractEmployee(4, "D", 15000, 2, 12);
            ContractEmployee e5 = new ContractEmployee(5, "E", 15000, 2, 12);

            Employee e6 = new InternEmployee(6, "F", 8000, 0);
            InternEmployee e7 = new InternEmployee(7, "G", 8000, 0);

            Console.WriteLine("Employee Annual Salary:");
            Console.WriteLine(e1.CalculateAnnualSalary());

            Console.WriteLine("\nPermanent Employee:");
            Console.WriteLine("Base: " + e2.CalculateAnnualSalary());
            Console.WriteLine("Derived: " + e3.CalculateAnnualSalary());

            Console.WriteLine("\nContract Employee:");
            Console.WriteLine("Base : " + e4.CalculateAnnualSalary());
            Console.WriteLine("Derived: " + e5.CalculateAnnualSalary());

            Console.WriteLine("\nIntern Employee:");
            Console.WriteLine("Base: " + e6.CalculateAnnualSalary());
            Console.WriteLine("Derived : " + e7.CalculateAnnualSalary());

        }
    }
}
