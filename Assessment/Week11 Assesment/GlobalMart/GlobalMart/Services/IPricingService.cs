namespace GlobalMart.Services
{
    public interface IPricingService
    {
        decimal CalculatePrice(decimal basePrice, string promoCode);
        decimal GetBasePrice();
        IEnumerable<string> GetAvailablePromoCodes();
    }
}
