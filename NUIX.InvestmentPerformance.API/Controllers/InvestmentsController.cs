using Microsoft.AspNetCore.Mvc;
using NUIX.InvestmentPerformance.API.DataTransferObjects;
using NUIX.InvestmentPerformance.API.Services;

namespace NUIX.InvestmentPerformance.API.Controllers
{
    [ApiController]
    [Route("api/investments")]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentService investmentService;

        public InvestmentsController(IInvestmentService investmentService)
        {
            // TODO - Explain what the constructor is doing here.
            this.investmentService = investmentService;
        }

        [HttpGet]
        [Route("api/investments/get-investments")]
        public ActionResult<List<InvestmentSummaryDTO>> GetInvestments(Guid userInvestmentID)
        {
            var investments = investmentService.GetInvestmentsForUser(userInvestmentID);
            return Ok(investments);
        }

        [HttpGet("api/investments/get-investments-details")]
        public ActionResult<InvestmentDetailDTO> GetInvestmentDetails(Guid userInvestmentID, Guid investmentId)
        {
            var details = investmentService.GetInvestmentDetails(userInvestmentID, investmentId);
            if (details == null)
                return NotFound();

            return Ok(details);
        }
    }
}
