using QueueManagement.Application.Interfaces;
using QueueManagement.Domain.Entities;
using QueueManagement.Persistence.Context;

namespace QueueManagement.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IRepository<Category> Categories { get; }

    public IRepository<SubCategory> SubCategories { get; }

    public IRepository<Token> Tokens { get; }

    public IRepository<TokenTransfer> TokenTransfers { get; }

    public IRepository<Counter> Counters { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Categories = new Repository<Category>(_context);

        SubCategories = new Repository<SubCategory>(_context);

        Tokens = new Repository<Token>(_context);
        TokenTransfers =
    new Repository<TokenTransfer>(_context);


        Counters = new Repository<Counter>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}