namespace KocFinansCC.Data.Repositories
{
    using System.Threading.Tasks;
    using Abstract;
    using KocFinansCC.Data.Context.Abstract;
    using Models;
    using MongoDB.Bson;

    public class CreditApproveRepository : ICreditApproveRepository
    {
        private readonly ICreditApproveContext _context;

        public CreditApproveRepository(ICreditApproveContext context)
        {
            _context = context;
        }

        public async Task Create(CreditApprove creditApprove)
        {
            await _context.CreditApproves.InsertOneAsync(creditApprove);
        }

        public async Task<long> GetNextId()
        {
            return await _context.CreditApproves.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}
