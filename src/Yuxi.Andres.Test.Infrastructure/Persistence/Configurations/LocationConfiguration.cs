using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Infrastructure.Persistence.Configurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<LocationAggregate>
    {
        public void Configure(EntityTypeBuilder<LocationAggregate> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name);
            builder.Property(l => l.Address);
            builder.Property(l => l.OpenDate);
            builder.Property(l => l.CloseDate);
        }
    }
}

