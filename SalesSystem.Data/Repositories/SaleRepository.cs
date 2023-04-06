using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Models;
using SalesSystem.Core.Repositories;
using System.Data.Common;
using System.Data;
using SalesSystem.Data.Repositories;

namespace SalesSystem.Data.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(SalesSystemDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await SalesSystemDbContext.Sales
                .ToListAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await SalesSystemDbContext.Sales
                .SingleOrDefaultAsync(m => m.SaleId == id);
        }

        private SalesSystemDbContext SalesSystemDbContext
        {
            get { return Context as SalesSystemDbContext; }
        }
    }
}