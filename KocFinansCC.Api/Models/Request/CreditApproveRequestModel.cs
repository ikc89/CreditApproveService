namespace KocFinansCC.Api.Models.Request
{
    public class CreditApproveRequestModel
    {
        public string CitizenNo { get; set; }

        public string NameSurname { get; set; }

        public int MonthlySalary { get; set; }

        public string PhoneNumber { get; set; }
    }
}
