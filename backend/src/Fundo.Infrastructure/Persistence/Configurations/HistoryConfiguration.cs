using Fundo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundo.Infrastructure.Persistence.Configurations;

public class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.LoandId)
            .IsRequired();

        entity.Property(e => e.Description)
            .IsRequired();
    }
}