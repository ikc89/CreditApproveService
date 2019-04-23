namespace KocFinansCC.Api.Controllers.V1
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Request;
    using Models.Response;
    using Services.Abstract;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CreditApproveController : ControllerBase
    {
        private readonly ICreditApproveService _creditApproveService;

        public CreditApproveController(ICreditApproveService creditApproveService)
        {
            _creditApproveService = creditApproveService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreditApproveResponseModel), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<CreditApproveResponseModel>> MakeCreditApply(CreditApproveRequestModel creditApproveRequest)
        {
            if (string.IsNullOrWhiteSpace(creditApproveRequest.CitizenNo))
            {
                return BadRequest("CitizenNo can not be null!");
            }

            if (creditApproveRequest.MonthlySalary <= 0)
            {
                return BadRequest("Monthly Salary should be greater than 0!");
            }

            return _creditApproveService.GetCreditApproveResult(creditApproveRequest).Result;
        }
    }
}
