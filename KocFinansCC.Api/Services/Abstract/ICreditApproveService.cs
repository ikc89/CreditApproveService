namespace KocFinansCC.Api.Services.Abstract
{
    using System.Threading.Tasks;
    using Models.Request;
    using Models.Response;

    public interface ICreditApproveService
    {
        Task<CreditApproveResponseModel> GetCreditApproveResult(CreditApproveRequestModel creditApproveRequest);
    }
}
