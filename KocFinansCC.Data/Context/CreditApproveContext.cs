namespace KocFinansCC.Data.Context
{
    using Abstract;
    using Config;
    using Models;
    using MongoDB.Driver;

    public class CreditApproveContext : ICreditApproveContext
    {
        private readonly IMongoDatabase _db;

        public CreditApproveContext(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.Database);
        }

        public IMongoCollection<CreditApprove> CreditApproves => _db.GetCollection<CreditApprove>("CreditApproves");
    }
}
