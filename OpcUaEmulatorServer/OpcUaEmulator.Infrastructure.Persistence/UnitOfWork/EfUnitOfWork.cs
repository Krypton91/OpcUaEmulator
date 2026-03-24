using OpcUaEmulator.Application.Common.Interfaces;
using OpcUaEmulator.Infrastructure.Persistence.Db;

namespace OpcUaEmulator.Infrastructure.Persistence.UnitOfWork
{
    public sealed class EfUnitOfWork : IUnitOfWork
    {
        private readonly OpcUaEmulatorDbContext _dbContext;

        public EfUnitOfWork(OpcUaEmulatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _dbContext.SaveChangesAsync(cancellationToken);
    }
}
