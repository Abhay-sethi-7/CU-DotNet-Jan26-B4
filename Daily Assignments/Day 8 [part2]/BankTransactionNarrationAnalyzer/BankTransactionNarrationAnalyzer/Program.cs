
namespace BankTransactionNarrationAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            string input = Console.ReadLine();

            string[] parts = input.Split('#');

            string transactionId = parts[0];
            string accountHolder = parts[1];
            string narration = parts[2];

            narration = narration.Trim();

            while (narration.Contains("  "))
            {
                narration = narration.Replace("  ", " ");
            }

            narration = narration.ToLower();

            bool hasKeyword =
                narration.Contains("deposit") ||
                narration.Contains("withdrawal") ||
                narration.Contains("transfer");

            string standardNarration = "cash deposit successful";
            bool isStandard = narration.Equals(standardNarration);

            string category;

            if (!hasKeyword)
            {
                category = "NON-FINANCIAL TRANSACTION";
            }
            else if (isStandard)
            {
                category = "STANDARD TRANSACTION";
            }
            else
            {
                category = "CUSTOM TRANSACTION";
            }
            Console.WriteLine("Transaction ID : " + transactionId);
            Console.WriteLine("Account Holder : " + accountHolder);
            Console.WriteLine("Narration : " + narration);
            Console.WriteLine("Category : " + category);
        }
    }
}
