using System.ComponentModel.DataAnnotations;

namespace Wealth_Track.ViewModel
{
    public class InvestmentCreateViewModel

    {

        [Required(ErrorMessage = "Ticker is required")]

        [StringLength(10)]

        [Display(Name = "Ticker Symbol")]

        public string TickerSymbol { get; set; }


        [Required(ErrorMessage = "Asset name is required")]
        
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }


        [Required]

        [Range(0.01, 1000000)]

        public decimal Price { get; set; }


        [Required]

        [Range(1, 10000)]

        public int Quantity { get; set; }


        [Display(Name = "Total Investment Value")]

        public string TotalValue => (Price * Quantity).ToString("C");

    }


}
