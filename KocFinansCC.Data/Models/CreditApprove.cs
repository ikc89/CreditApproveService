namespace KocFinansCC.Data.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class CreditApprove
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public long Id { get; set; }

        public string CitizenNo { get; set; }

        public string NameSurname { get; set; }

        public int MonthlySalary { get; set; }

        public string PhoneNumber { get; set; }

        public string ApproveStatus { get; set; }

        public int CreditAmount { get; set; }
    }
}
