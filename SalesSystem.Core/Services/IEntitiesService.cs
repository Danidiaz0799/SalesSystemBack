using SalesSystem.Core.Models;

namespace SalesSystem.Core.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSales();
        Task<Sale> GetSaleById(int id);
        Task<Sale> CreateSale(Sale newSale);
        Task<bool> UpdateSale(Sale brandToBeUpdated, Sale brand);
        Task DeleteSale(Sale sale);

    }
}
