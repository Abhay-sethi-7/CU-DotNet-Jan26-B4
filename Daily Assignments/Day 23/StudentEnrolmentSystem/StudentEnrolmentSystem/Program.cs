using System;

namespace StudentEnrollmentSystem
{
    // Custom exception for invalid age
    class InvalidStudentAgeException : Exception
    {
        public InvalidStudentAgeException(string message) : base(message)
        {
        }
    }

    // Custom exception for invalid name
    class InvalidStudentNameException : Exception
    {
        public InvalidStudentNameException(string message) : base(message)
        {
        }
    }

    class Program
    {
        static void Main()
        {
          
            try
            {
                Console.Write("Enter numerator: ");
                int num1 = int.Parse(Console.ReadLine());

                Console.Write("Enter denominator: ");
                int num2 = int.Parse(Console.ReadLine());

                int result = num1 / num2;
                Console.WriteLine("Result = " + result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Division by zero is not allowed.");
            }
            finally
            {
                Console.WriteLine("CW From Devide by zero Block :Operation Completed\n");
            }

            try
            {
                Console.Write("Enter an integer value: ");
                int number = int.Parse(Console.ReadLine());

                Console.WriteLine("You entered: " + number);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid integer.");
            }
            finally
            {
                Console.WriteLine("CW From Format [integer conversion] Block :Operation Completed\n");
            }

            try
            {
                int[] numbers = { 10, 20, 30 };

                Console.Write("Enter array index (0 to 2): ");
                int index = int.Parse(Console.ReadLine());

                Console.WriteLine("Value at index = " + numbers[index]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error: Index is outside the array range.");
            }
            finally
            {
                Console.WriteLine("CW From Array index out of rangeblock :Operation Completed\n");
            }

            GetValidStudentDetails();

            Console.WriteLine("Program Ended Successfully.");
        }

        static void GetValidStudentDetails()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Student Name: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        throw new InvalidStudentNameException(
                            "Student name cannot be empty."
                        );
                    }

                    Console.WriteLine("Name accepted.");
                    break;
                }
                catch (InvalidStudentNameException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Enter Student Age: ");
                    int age = int.Parse(Console.ReadLine());

                    if (age < 18 || age > 60)
                    {
                        throw new InvalidStudentAgeException(
                            "Student age must be between 18 and 60."
                        );
                    }

                    Console.WriteLine("Age accepted.");
                    break;
                }
                catch (InvalidStudentAgeException ex)
                {
                    Exception newException =
                        new Exception("Student validation failed.", ex);

                    Console.WriteLine("\nException Message:");
                    Console.WriteLine(newException.Message);

                    Console.WriteLine("Inner Exception Message:");
                    Console.WriteLine(newException.InnerException.Message);

                    LogException(newException);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error: Age must be a number.");
                    LogException(ex);
                }
            }
        }

        static void LogException(Exception ex)
        {
            Console.WriteLine("--- Exception Details ---");
            Console.WriteLine("Message: " + ex.Message);
            Console.WriteLine("StackTrace: " + ex.StackTrace);

            if (ex.InnerException != null)
                Console.WriteLine("InnerException: " + ex.InnerException.Message);
            else
                Console.WriteLine("InnerException: None");

            Console.WriteLine("-------------------------\n");
        }
    }
}
