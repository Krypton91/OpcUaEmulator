using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpcUaEmulator.Domain.Entities;

namespace OpcUaEmulator.Infrastructure.Persistence.Configurations
{
    public sealed class EmulatedNodeConfiguration : IEntityTypeConfiguration<EmulatedNode>
    {
        public void Configure(EntityTypeBuilder<EmulatedNode> builder)
        {
            builder.ToTable("emulated_nodes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NodeId)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.BrowseName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.DataType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CurrentValue)
                .HasColumnType("text");

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();

            builder.Property(x => x.UpdatedAtUtc);

            builder.HasIndex(x => x.NodeId)
                .IsUnique();
        }
    }
}