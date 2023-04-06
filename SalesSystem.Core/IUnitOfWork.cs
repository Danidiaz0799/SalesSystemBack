using SalesSystem.Core.Repositories;

namespace SalesSystem.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISaleRepository Sale { get; }
        Task<int> CommitAsync();
    }
}
