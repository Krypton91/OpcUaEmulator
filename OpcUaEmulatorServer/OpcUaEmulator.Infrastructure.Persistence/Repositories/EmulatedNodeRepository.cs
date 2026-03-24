using Microsoft.EntityFrameworkCore;
using OpcUaEmulator.Application.Common.Interfaces;
using OpcUaEmulator.Domain.Entities;
using OpcUaEmulator.Infrastructure.Persistence.Db;

namespace OpcUaEmulator.Infrastructure.Persistence.Repositories
{
    public sealed class EmulatedNodeRepository : IEmulatedNodeRepository
    {
        private readonly OpcUaEmulatorDbContext _dbContext;

        public EmulatedNodeRepository(OpcUaEmulatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<EmulatedNode?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.EmulatedNodes
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<EmulatedNode?> GetByNodeIdAsync(string nodeId, CancellationToken cancellationToken = default)
        {
            return _dbContext.EmulatedNodes
                .FirstOrDefaultAsync(x => x.NodeId == nodeId, cancellationToken);
        }

        public Task<bool> ExistsByNodeIdAsync(string nodeId, CancellationToken cancellationToken = default)
        {
            return _dbContext.EmulatedNodes
                .AnyAsync(x => x.NodeId == nodeId, cancellationToken);
        }

        public Task AddAsync(EmulatedNode entity, CancellationToken cancellationToken = default)
        {
            return _dbContext.EmulatedNodes.AddAsync(entity, cancellationToken).AsTask();
        }
    }
}