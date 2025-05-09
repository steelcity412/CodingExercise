namespace NUIX.InvestmentPerformance.API.Models
{
    public class Investment
    {
        public Guid InvestmentID { get; set; }
        public string InvestmentName { get; set; }
        public decimal PurchasePricePerShare { get; set; }
        public int NumberOfShares { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CurrentPricePerShare { get; set; }
    }
}
