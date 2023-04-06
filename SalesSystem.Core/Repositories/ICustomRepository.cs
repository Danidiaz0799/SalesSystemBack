using SalesSystem.Core.Models;

namespace SalesSystem.Core.Repositories
{
    public interface ISaleRepository : IRepositoryGeneral<Sale>
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale> GetSaleByIdAsync(int id);
    }
}
