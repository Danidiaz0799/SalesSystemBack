using System.Threading.Tasks;
using SalesSystem.Core;
using SalesSystem.Core.Repositories;
using SalesSystem.Data.Repositories;

namespace SalesSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(SalesSystemDbContext context)
        {
            this.Context = context;
        }
        //database context
        private readonly SalesSystemDbContext Context;

        //database entities
        private SaleRepository _SaleRepository;

        public ISaleRepository Sale => _SaleRepository = _SaleRepository ?? new SaleRepository(Context);

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}