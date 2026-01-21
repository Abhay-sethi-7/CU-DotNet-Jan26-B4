namespace GymFees
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateGymMembership(true, false, true));

        }

        static double CalculateGymMembership(bool treadmill, bool weightLifting, bool zumba)
        {
            double amount = 1000;
            bool serviceSelected = false;

            if (treadmill)
            {
                amount += 300;
                serviceSelected = true;
            }

            if (weightLifting)
            {
                amount += 500;
                serviceSelected = true;
            }

            if (zumba)
            {
                amount += 250;
                serviceSelected = true;
            }

            if (!serviceSelected)
                amount += 200;

            amount += amount * 0.05;
            return amount;
        }

    }
}
