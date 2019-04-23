namespace KocFinansCC.Data.Context.Abstract
{
    using Models;
    using MongoDB.Driver;

    public interface ICreditApproveContext
    {
        IMongoCollection<CreditApprove> CreditApproves { get; }
    }
}
