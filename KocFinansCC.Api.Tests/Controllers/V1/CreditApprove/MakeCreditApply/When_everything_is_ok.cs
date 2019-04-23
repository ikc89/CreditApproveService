namespace KocFinansCC.Api.Tests.Controllers.V1.CreditApprove.MakeCreditApply
{
    using System.Net;
    using FluentAssertions;
    using Models.Request;
    using Xunit;

    public class When_everything_is_ok : Given
    {
        [Fact]
        public async void Should_get_recommendations()
        {
            var requestModel = new CreditApproveRequestModel
            {
                CitizenNo = "CitizenNo",
                MonthlySalary = 5000,
                NameSurname = "NameSurname",
                PhoneNumber = "PhoneNumber"
            };
            var response = await CreditApproveController.MakeCreditApply(requestModel);
            response.Value.CreditAmount.Should().Be(10000);
        }

        [Fact]
        public async void Should_get_bad_request_if_citizen_no_is_null_or_empty()
        {
            var requestModel = new CreditApproveRequestModel();
            requestModel.CitizenNo = string.Empty;
            var response = await CreditApproveController.MakeCreditApply(requestModel);
            response.Value.Should().BeNull();
        }

        [Fact]
        public async void Should_get_bad_request_if_salary_smaller_than_or_equals_zero()
        {
            var requestModel = new CreditApproveRequestModel
            {
                CitizenNo = "CitizenNo",
                MonthlySalary = 0,
                NameSurname = "NameSurname",
                PhoneNumber = "PhoneNumber"
            };
            requestModel.CitizenNo = string.Empty;
            var response = await CreditApproveController.MakeCreditApply(requestModel);
            response.Value.Should().BeNull();
        }
    }
}
