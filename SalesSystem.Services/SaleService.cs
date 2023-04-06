using SalesSystem.Core;
using SalesSystem.Core.Models;
using SalesSystem.Core.Services;

namespace SalesSystem.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaleService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _unitOfWork.Sale
                .GetAllSalesAsync();
        }
        public async Task<Sale> GetSaleById(int id)
        {
            return await _unitOfWork.Sale
                .GetSaleByIdAsync(id);
        }

        public async Task<Sale> CreateSale(Sale newSale)
        {
            await _unitOfWork.Sale.AddAsync(newSale);
            await _unitOfWork.CommitAsync();
            return newSale;
        }

        public async Task<bool> UpdateSale(Sale saleToBeUpdated, Sale sale)
        {
            return await _unitOfWork.Sale
                .UpdateAsync(saleToBeUpdated, sale);
        }

        public async Task DeleteSale(Sale sale)
        {
            _unitOfWork.Sale.Remove(sale);
            await _unitOfWork.CommitAsync();
        }
    }
}