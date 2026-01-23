namespace ApplicationConfig
{   
    class ApplictionConfigg
    {
        public static string ApplicationName;
        public static string Environment;
        public static int AccessCount;
        public static bool IsIntitalized;
        
        static ApplictionConfigg()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            AccessCount = 0;
            IsIntitalized = false;
            Console.WriteLine("Static constructor executed");
        }
        public  static void Initialize(string appName ,string environment)
        {
            ApplicationName= appName;
            Environment = environment;
            IsIntitalized= true;
            AccessCount++;
        }
        public static string GetConfigurationSummary()
        {
              AccessCount++;
            return $"Application Name is {ApplicationName}," +
                $" Environment is {Environment} ," +
                $"Access Count is {AccessCount}," +
                $"Initialization Status is {IsIntitalized}";
        }
        public static void resetConfiguration()
        {
            
            ApplicationName = "MyApp";
            Environment = "Development";
            AccessCount = 0;
            IsIntitalized = false;
            
            Console.WriteLine("Resetting to default Configuration:");
            Console.WriteLine($"Application Name is {ApplicationName}," +
                $" Environment is {Environment} ," +
                $"Access Count is {AccessCount}," +
                $"Initialization Status is {IsIntitalized}");
            AccessCount++;
            Console.WriteLine($"During Reset Access Count is {AccessCount}" );
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ApplictionConfigg.ApplicationName);
            Console.WriteLine(ApplictionConfigg.GetConfigurationSummary());
            ApplictionConfigg.Initialize("DemoApp", "QA");

            Console.WriteLine(ApplictionConfigg.GetConfigurationSummary());
            Console.WriteLine(ApplictionConfigg.GetConfigurationSummary());
            Console.WriteLine(ApplictionConfigg.GetConfigurationSummary());
            Console.WriteLine(ApplictionConfigg.GetConfigurationSummary());
            ApplictionConfigg.resetConfiguration();

            Console.WriteLine("\nAfter Reset:");
            Console.WriteLine(ApplictionConfigg.GetConfigurationSummary());

        }
    }
}
