using OpcUaEmulator.Domain.Entities;

namespace OpcUaEmulator.Application.Common.Interfaces
{
    public interface IEmulatedNodeRepository
    {
        Task<EmulatedNode?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<EmulatedNode?> GetByNodeIdAsync(string nodeId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNodeIdAsync(string nodeId, CancellationToken cancellationToken = default);
        Task AddAsync(EmulatedNode entity, CancellationToken cancellationToken = default);
    }
}