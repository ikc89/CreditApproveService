namespace KocFinansCC.Data.Repositories.Abstract
{
    using System.Threading.Tasks;
    using Models;

    public interface ICreditApproveRepository
    {
        Task Create(CreditApprove creditApprove);

        Task<long> GetNextId();
    }
}
