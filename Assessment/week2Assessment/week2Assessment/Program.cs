namespace week2Assessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] policyHolderNames = new string[5];
            decimal[] annualPremiums = new decimal[5];

            decimal totalPremium = 0;
            decimal averagePremium;
            decimal highestPremium;
            decimal lowestPremium;
            for (int i = 0; i < 5; i++)
            {

                while (true)
                {
                    Console.WriteLine($"Enter name of policy holder {i + 1}: ");
                    policyHolderNames[i] = Console.ReadLine();

                    if (policyHolderNames[i] == "")
                    {
                        Console.WriteLine("Name cannot be empty.");
                        continue;
                    }

                    bool isValidName = true;

                    for (int j = 0; j < policyHolderNames[i].Length; j++)
                    {
                        if (!char.IsLetter(policyHolderNames[i][j]))
                        {
                            isValidName = false;
                            break;
                        }
                    }

                    if (isValidName)
                        break;

                    Console.WriteLine("Name must contain only alphabets.");
                }

                while (true)
                {
                    Console.WriteLine($"Enter annual premium for {policyHolderNames[i]}: ");

                    try
                    {
                        decimal premium = decimal.Parse(Console.ReadLine());

                        if (premium > 0)
                        {
                            annualPremiums[i] = premium;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Premium must be greater than 0.");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Premium must be a numeric value.");
                    }
                }

            }

            highestPremium = annualPremiums[0];
            lowestPremium = annualPremiums[0];
            for (int i = 0; i < 5; i++)
            {
                totalPremium = totalPremium + annualPremiums[i];
                if (annualPremiums[i] > highestPremium)
                {
                    highestPremium = annualPremiums[i];
                }
                if (annualPremiums[i] < lowestPremium)
                {
                    lowestPremium = annualPremiums[i];
                }
            }
                averagePremium = totalPremium / 5;
            
                Console.WriteLine("Insurance Premium Summary report");  
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"{"Name",-15}{"Premium",-15}{"Category"}");
                Console.WriteLine("-----------------------------------------------");


                for (int j = 0; j < 5; j++)
                {
                    string category;
                    if (annualPremiums[j] < 10000)
                        category = "LOW";
                    else if (annualPremiums[j] <= 25000)
                        category = "MEDIUM";
                    else
                        category = "HIGH";
                Console.WriteLine(
                                $"{policyHolderNames[j].ToUpper(),-15}" +
                                        $"{annualPremiums[j],-15:F2}" +
                                            $"{category}"
                                                           );

            }
            Console.WriteLine("-----------------------------------------");
                Console.WriteLine($"Total Preimum {totalPremium.ToString("F2")}");
                Console.WriteLine($"Average Preimum {averagePremium.ToString("F2")}");
                Console.WriteLine($"Highest Preimum {highestPremium.ToString("F2")}");
                Console.WriteLine($"Lowest Preimum {lowestPremium.ToString("F2")}");
            }
        }
    }

