using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUaEmulator.Domain.Entities
{
    public sealed class EmulatedNode
    {
        public Guid Id { get; private set; }
        public string NodeId { get; private set; } = null!;
        public string BrowseName { get; private set; } = null!;
        public string DataType { get; private set; } = null!;
        public string? CurrentValue { get; private set; }
        public DateTimeOffset CreatedAtUtc { get; private set; }
        public DateTimeOffset? UpdatedAtUtc { get; private set; }

        public EmulatedNode(
            Guid id,
            string nodeId,
            string browseName,
            string dataType,
            string? currentValue = null)
        {
            Id = id;
            NodeId = nodeId;
            BrowseName = browseName;
            DataType = dataType;
            CurrentValue = currentValue;
            CreatedAtUtc = DateTimeOffset.UtcNow;
        }

        public void UpdateValue(string? value)
        {
            CurrentValue = value;
            UpdatedAtUtc = DateTimeOffset.UtcNow;
        }
    }
}