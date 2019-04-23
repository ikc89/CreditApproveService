namespace KocFinansCC.Api.Models.Response
{
    using Enums;

    public class CreditApproveResponseModel
    {
        public ApproveStatusEnum ApproveStatus { get; set; }

        public int CreditAmount { get; set; }
    }
}
