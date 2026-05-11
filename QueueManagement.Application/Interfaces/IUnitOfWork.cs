using QueueManagement.Domain.Entities;

namespace QueueManagement.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Category> Categories { get; }

    IRepository<SubCategory> SubCategories { get; }

    IRepository<Token> Tokens { get; }


    IRepository<TokenTransfer> TokenTransfers { get; }
    IRepository<Counter> Counters { get; }

    Task<int> SaveChangesAsync();
}