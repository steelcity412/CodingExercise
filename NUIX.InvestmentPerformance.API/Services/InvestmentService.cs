using NUIX.InvestmentPerformance.API.DataTransferObjects;
using NUIX.InvestmentPerformance.API.Models;
using NUIX.InvestmentPerformance.API.Models.Enums;

namespace NUIX.InvestmentPerformance.API.Services
{
    public class InvestmentService : IInvestmentService
    {
        // TODO : Ask them if the want me to setup a database instead of having to use mock data.
        private readonly List<UserInvestment> _userInvestments = new()
        {
            new UserInvestment
            {
                UserInvestmentID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserInvestments = new List<Investment>
                {
                    new Investment
                    {
                        InvestmentID = Guid.Parse("b4a35a0a-1815-4439-b9f6-60f786e1a227"),
                        InvestmentName = "Tesla",
                        NumberOfShares = 10,
                        PurchasePricePerShare = 150,
                        CurrentPricePerShare = 180,
                        PurchaseDate = DateTime.UtcNow.AddMonths(-14)
                    },
                    new Investment
                    {
                        InvestmentID = Guid.Parse("04c031fa-f23d-4a73-90a7-735c94db2452"),
                        InvestmentName = "Apple",
                        NumberOfShares = 20,
                        PurchasePricePerShare = 120,
                        CurrentPricePerShare = 110,
                        PurchaseDate = DateTime.UtcNow.AddMonths(-10)
                    }
                }
            }
        };


        public List<InvestmentSummaryDTO> GetInvestmentsForUser(Guid userInvestmentID)
        {
            var user = _userInvestments.FirstOrDefault(x => x.UserInvestmentID == userInvestmentID);

            if (user == null) return new();

            return user.UserInvestments.Select(i => new InvestmentSummaryDTO
            {
                InvestmentID = i.InvestmentID,
                InvestmentName = i.InvestmentName
            }).ToList();
        }

        public InvestmentDetailDTO? GetInvestmentDetails(Guid userInvestmentID, Guid investmentID)
        {
            var user = _userInvestments.FirstOrDefault(x => x.UserInvestmentID == userInvestmentID);

            var investment = user?.UserInvestments.FirstOrDefault(i => i.InvestmentID == investmentID);

            if (investment == null) return null;

            decimal currentValue = investment.NumberOfShares * investment.CurrentPricePerShare;
            
            decimal totalCost = investment.NumberOfShares * investment.PurchasePricePerShare;
            
            decimal gainOrLoss = currentValue - totalCost;

            var term = (DateTime.UtcNow - investment.PurchaseDate).TotalDays <= 365 ? InvestmentTerm.ShortTerm : InvestmentTerm.LongTerm;

            return new InvestmentDetailDTO
            {
                InvestmentID = investment.InvestmentID,
                InvestmentName = investment.InvestmentName,
                NumberOfShares = investment.NumberOfShares,
                CostBasisPerShare = investment.PurchasePricePerShare,
                CurrentPrice = investment.CurrentPricePerShare,
                CurrentValue = currentValue,
                TotalGainOrLoss = gainOrLoss,
                Term = term
            };
        }
    }
}
