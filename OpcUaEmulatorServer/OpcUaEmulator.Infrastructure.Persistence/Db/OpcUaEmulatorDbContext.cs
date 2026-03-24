using Microsoft.EntityFrameworkCore;
using OpcUaEmulator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUaEmulator.Infrastructure.Persistence.Db
{
    public sealed class OpcUaEmulatorDbContext : DbContext
    {
        public const string DefaultSchema = "opcua";

        public OpcUaEmulatorDbContext(DbContextOptions<OpcUaEmulatorDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmulatedNode> EmulatedNodes => Set<EmulatedNode>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OpcUaEmulatorDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
