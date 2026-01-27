using System;

namespace LoanEMI
{
    // Base Class
    class Loan
    {
        public string LoanNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal PrincipalAmount { get; set; }
        public int TenureInYears { get; set; }

        // Parameterized Constructor
        public Loan(string loanNo, string customer, decimal principal, int tenure)
        {
            LoanNumber = loanNo;
            CustomerName = customer;
            PrincipalAmount = principal;
            TenureInYears = tenure;
        }

        // Base class EMI calculation (10% simple interest)
        public double CalculateEMI()
        {
            decimal rate = 10m;

            decimal simpleInterest =
                (PrincipalAmount * rate * TenureInYears) / 100;

            decimal totalAmount = PrincipalAmount + simpleInterest;
            int totalMonths = TenureInYears * 12;

            decimal emi = totalAmount / totalMonths;
            return (double)emi;
        }
    }

    // Derived Class - HomeLoan
    class HomeLoan : Loan
    {
        public HomeLoan(string loanNo, string customer, decimal principal, int tenure)
            : base(loanNo, customer, principal, tenure)
        {
        }

        // Method Hiding (NOT overriding)
        public new double CalculateEMI()
        {
            decimal interestRate = 8m;
            decimal processingRate = 1m;

            decimal interest =
                (PrincipalAmount * interestRate * TenureInYears) / 100;

            decimal processingFee =
                (PrincipalAmount * processingRate) / 100;

            decimal totalAmount =
                PrincipalAmount + interest + processingFee;

            int totalMonths = TenureInYears * 12;
            decimal emi = totalAmount / totalMonths;

            return (double)emi;
        }
    }

    // Derived Class - CarLoan
    class CarLoan : Loan
    {
        public CarLoan(string loanNo, string customer, decimal principal, int tenure)
            : base(loanNo, customer, principal, tenure)
        {
        }

        // Method Hiding (NOT overriding)
        public new double CalculateEMI()
        {
            decimal interestRate = 9m;
            decimal insuranceAmount = 15000m;

            decimal effectivePrincipal = PrincipalAmount + insuranceAmount;

            decimal interest =
                (effectivePrincipal * interestRate * TenureInYears) / 100;

            decimal totalAmount =
                effectivePrincipal + interest;

            int totalMonths = TenureInYears * 12;
            decimal emi = totalAmount / totalMonths;

            return (double)emi;
        }
    }

    // Main Class
    internal class Program
    {
        static void Main()
        {
            Loan[] loans = new Loan[]
            {
                new HomeLoan("HL001", "Amit", 500000, 10),
                new HomeLoan("HL002", "Neha", 600000, 15),
                new CarLoan("CL001", "Rahul", 400000, 5),
                new CarLoan("CL002", "Pooja", 350000, 4)
            };

            Console.WriteLine("EMI Calculation (Base Class Method Called):\n");

            for (int i = 0; i < loans.Length; i++)
            {
                Console.WriteLine(
                    $"Loan No: {loans[i].LoanNumber}, EMI: {loans[i].CalculateEMI():F2}");
            }

            Console.ReadLine();
        }
    }
}
