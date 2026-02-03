

namespace SafeDrivePolicyOptimizer
{
  
    class Policy
    {
        public string HolderName { get; set; }
        public decimal Premium { get; set; }     
        public int RiskScore { get; set; }       
        public DateTime RenewalDate { get; set; }

        public override string ToString()
        {
            return $"Holder: {HolderName}, Premium: {Premium:C}, RiskScore: {RiskScore}, RenewalDate: {RenewalDate:d}";
        }
    }

    class PolicyTracker
    {
        private Dictionary<string, Policy> policies = new Dictionary<string, Policy>();

        public void AddPolicy(string policyId, Policy policy)
        {
            policies[policyId] = policy;
        }
        public void ApplyBulkAdjustment()
        {
            foreach (var item in policies)
            {
                Policy policy = item.Value;

                if (policy.RiskScore > 75)
                {
                    policy.Premium += policy.Premium * 0.05m;
                }
            }
        }
        public void CleanupOldPolicies()
        {
            List<string> keysToRemove = new List<string>();
            DateTime cutoffDate = DateTime.Now.AddYears(-3);

            foreach (var item in policies)
            {
                if (item.Value.RenewalDate < cutoffDate)
                {
                    keysToRemove.Add(item.Key);
                }
            }

            foreach (string key in keysToRemove)
            {
                policies.Remove(key);
            }
        }
        public string GetPolicyDetails(string policyId)
        {
            if (policies.TryGetValue(policyId, out Policy policy))
            {
                return policy.ToString();
            }

            return "Policy Not Found";
        }
        public void DisplayAllPolicies()
        {
            foreach (var item in policies)
            {
                Console.WriteLine($"Policy ID: {item.Key} | {item.Value}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PolicyTracker tracker = new PolicyTracker();

            tracker.AddPolicy("POL001", new Policy
            {
                HolderName = "Abhay Sethi",
                Premium = 12000m,
                RiskScore = 82,
                RenewalDate = new DateTime(2026, 5, 10)
            });

            tracker.AddPolicy("POL002", new Policy
            {
                HolderName = "Riya",
                Premium = 9000m,
                RiskScore = 60,
                RenewalDate = DateTime.Now
            });

            tracker.AddPolicy("POL003", new Policy
            {
                HolderName = "Karan",
                Premium = 15000m,
                RiskScore = 90,
                RenewalDate = new DateTime(2026, 3, 15)
            });

            Console.WriteLine("=== Before Bulk Adjustment ===");
            tracker.DisplayAllPolicies();

            tracker.ApplyBulkAdjustment();

            Console.WriteLine("\n=== After Bulk Adjustment ===");
            tracker.DisplayAllPolicies();

            tracker.CleanupOldPolicies();

            Console.WriteLine("\n=== After Cleanup ===");
            tracker.DisplayAllPolicies();

            Console.WriteLine("\n=== Security Check ===");
            Console.WriteLine(tracker.GetPolicyDetails("POL002"));
            Console.WriteLine(tracker.GetPolicyDetails("POL999"));

            Console.ReadLine();
        }
    }
}
