namespace SmartBank.Services.Helpers
{
    public static class AccountNumberGenerator
    {
        public static string Generate(int accountId)
        {
            var year = DateTime.Now.Year;

            var paddedId = accountId.ToString("D6");

            return $"SB-{year}-{paddedId}";
        }
    }
}
