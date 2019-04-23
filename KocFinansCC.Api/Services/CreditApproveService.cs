namespace KocFinansCC.Api.Services
{
    using System;
    using System.Threading.Tasks;
    using Abstract;
    using Common.Services.Abstract;
    using Enums;
    using KocFinansCC.Data.Models;
    using KocFinansCC.Data.Repositories.Abstract;
    using Microsoft.Extensions.Configuration;
    using Models.Request;
    using Models.Response;

    public class CreditApproveService : ICreditApproveService
    {
        private readonly ICreditScoreService _creditScoreService;
        private readonly ISMSService _smsService;
        private readonly ICreditApproveRepository _creditApproveRepository;
        private readonly IConfiguration _configuration;

        public CreditApproveService(ICreditScoreService creditScoreService, ISMSService smsService, ICreditApproveRepository creditApproveRepository, IConfiguration configuration)
        {
            _creditScoreService = creditScoreService;
            _smsService = smsService;
            _creditApproveRepository = creditApproveRepository;
            _configuration = configuration;
        }

        public async Task<CreditApproveResponseModel> GetCreditApproveResult(CreditApproveRequestModel creditApproveRequest)
        {
            var result = new CreditApproveResponseModel();
            var creditScore = _creditScoreService.GetCreditScore(creditApproveRequest.CitizenNo);

            if (creditScore < 500)
            {
                result.ApproveStatus = ApproveStatusEnum.Rejected;
                return result;
            }

            if (creditScore >= 500 && creditScore < 1000 && creditApproveRequest.MonthlySalary < 5000)
            {
                result.ApproveStatus = ApproveStatusEnum.Approved;
                result.CreditAmount = 10000;
            }

            if (creditScore >= 1000)
            {
                result.ApproveStatus = ApproveStatusEnum.Approved;
                result.CreditAmount = creditApproveRequest.MonthlySalary * Convert.ToInt32(_configuration.GetSection("CreditLimitMultiplyFactor").Value);
            }

            var creditApprove = new CreditApprove();
            creditApprove.Id = await _creditApproveRepository.GetNextId();
            creditApprove.CitizenNo = creditApproveRequest.CitizenNo;
            creditApprove.NameSurname = creditApproveRequest.NameSurname;
            creditApprove.MonthlySalary = creditApproveRequest.MonthlySalary;
            creditApprove.PhoneNumber = creditApproveRequest.PhoneNumber;
            creditApprove.ApproveStatus = ApproveStatusEnum.Approved.ToString();
            creditApprove.CreditAmount = result.CreditAmount;
            await _creditApproveRepository.Create(creditApprove);

            var isSMSSent = _smsService.SendSMS(creditApproveRequest.PhoneNumber);
            return result;
        }
    }
}
