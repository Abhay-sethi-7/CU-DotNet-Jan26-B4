
namespace Employee_Annual_Performance_Bonus_Calculation_System
{
    public class InvalidPerformanceRatingException : Exception
    {
        public InvalidPerformanceRatingException()
            : base("Performance rating must be between 1 and 5.")
        {
        }
    }

    public class InvalidAttendanceException : Exception
    {
        public InvalidAttendanceException()
            : base("Attendance percentage must be between 0 and 100.")
        {
        }
    }
    public class EmployeeBonus
    {
        public decimal BaseSalary { get; set; }
        public int PerformanceRating { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal DepartmentMultiplier { get; set; }
        public double AttendancePercentage { get; set; }

        public decimal NetAnnualBonus
        {
            get
            {
                if (BaseSalary <= 0)
                    return 0;

                if (PerformanceRating < 1 || PerformanceRating > 5)
                    throw new InvalidPerformanceRatingException();

                if (AttendancePercentage < 0 || AttendancePercentage > 100)
                    throw new InvalidAttendanceException(); 

                decimal bonus = 0;

                decimal ratingPercentage;

                switch (PerformanceRating)
                {
                    case 5:
                        ratingPercentage = 0.25m;
                        break;

                    case 4:
                        ratingPercentage = 0.18m;
                        break;

                    case 3:
                        ratingPercentage = 0.12m;
                        break;

                    case 2:
                        ratingPercentage = 0.05m;
                        break;

                    case 1:
                        ratingPercentage = 0.00m;
                        break;

                    default:
                        ratingPercentage = 0;
                        break;
                }

                bonus = BaseSalary * ratingPercentage;

                if (YearsOfExperience > 10)
                    bonus += BaseSalary * 0.05m;
                else if (YearsOfExperience > 5)
                    bonus += BaseSalary * 0.03m;

                if (AttendancePercentage < 85)
                    bonus *= 0.80m;

                bonus *= DepartmentMultiplier;

               
                decimal maxBonus = BaseSalary * 0.40m;
                if (bonus > maxBonus)
                    bonus = maxBonus;

                decimal taxRate = 0;

                if (bonus <= 150000)
                    taxRate = 0.10m;
                else if (bonus <= 300000)
                    taxRate = 0.20m;
                else
                    taxRate = 0.30m;

                decimal finalBonus = bonus - (bonus * taxRate);

                return Math.Round(finalBonus, 2);
            }
        }
    }
}
