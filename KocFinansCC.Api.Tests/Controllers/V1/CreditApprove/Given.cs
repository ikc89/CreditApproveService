using KocFinansCC.Api.Enums;

namespace KocFinansCC.Api.Tests.Controllers.V1.CreditApprove
{
    using KocFinansCC.Api.Controllers.V1;
    using Models.Request;
    using Models.Response;
    using Moq;
    using Services.Abstract;

    public class Given
    {
        protected Mock<ICreditApproveService> creditApproveService;

        protected CreditApproveController CreditApproveController;
        protected CreditApproveResponseModel creditApproveResponseModel;

        public Given()
        {
            creditApproveService = new Mock<ICreditApproveService>();
            creditApproveResponseModel = new CreditApproveResponseModel
            {
                ApproveStatus = ApproveStatusEnum.Approved,
                CreditAmount = 10000
            };
            creditApproveService.Setup(x => x.GetCreditApproveResult(It.IsAny<CreditApproveRequestModel>())).ReturnsAsync(creditApproveResponseModel);
            CreditApproveController = new CreditApproveController(creditApproveService.Object);
        }
    }
}
