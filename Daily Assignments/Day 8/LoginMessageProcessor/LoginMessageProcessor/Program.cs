using System;

namespace LoginMessageProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            string[] parts = input.Split('|');

            string userName = parts[0];
            string loginMessage = parts[1];

            string cleanedMessage = loginMessage.Trim().ToLower();

            string standardMessage = "login successful";

            bool containsSuccessful = cleanedMessage.Contains("successful");

            string status;

            if (!containsSuccessful)
            {
                status = "LOGIN FAILED";
            }
            else if (cleanedMessage.Equals(standardMessage))
            {
                status = "LOGIN SUCCESS";
            }
            else
            {
                status = "LOGIN SUCCESS (wellcome!)";
            }

            Console.WriteLine($"Status  : {status}");
        }
    }
}

