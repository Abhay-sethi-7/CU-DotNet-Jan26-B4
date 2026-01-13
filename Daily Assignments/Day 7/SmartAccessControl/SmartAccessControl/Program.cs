using System;

namespace SmartAccessControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] data = input.Split('|');

            if (data.Length != 5)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            string gateCode = data[0];
            string userStr = data[1];
            string accessStr = data[2];
            string activeStr = data[3];
            string attemptsStr = data[4];

            // GateCode validation
            if (gateCode.Length != 2 || !char.IsLetter(gateCode[0]) || !char.IsDigit(gateCode[1]))
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // UserInitial validation
            if (userStr.Length != 1 || userStr[0] < 'A' || userStr[0] > 'Z')
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }
            char userInitial = userStr[0];

            // AccessLevel validation (1-7)
            if (accessStr.Length != 1 || accessStr[0] < '1' || accessStr[0] > '7')
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }
            byte accessLevel = (byte)(accessStr[0] - '0');

            // IsActive validation
            bool isActive;
            if (activeStr.ToLower() == "true")
                isActive = true;
            else if (activeStr.ToLower() == "false")
                isActive = false;
            else
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            // Attempts validation (0-200)
            int attemptsInt = 0;
            bool validAttempts = true;
            foreach (char c in attemptsStr)
            {
                if (c < '0' || c > '9')
                {
                    validAttempts = false;
                    break;
                }
                attemptsInt = attemptsInt * 10 + (c - '0');
            }

            if (!validAttempts || attemptsInt < 0 || attemptsInt > 200)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }
            byte attempts = (byte)attemptsInt;

            // Determine status
            string status;
            if (!isActive)
                status = "ACCESS DENIED – INACTIVE USER";
            else if (attempts > 100)
                status = "ACCESS DENIED – TOO MANY ATTEMPTS";
            else if (accessLevel >= 5)
                status = "ACCESS GRANTED – HIGH SECURITY";
            else
                status = "ACCESS GRANTED – STANDARD";

        
            Console.WriteLine("Status".PadRight(10) + ": " + status);
        }
    }
}
