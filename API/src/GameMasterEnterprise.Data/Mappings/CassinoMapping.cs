using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class CassinoMapping : IEntityTypeConfiguration<Cassino>
    {
        public void Configure(EntityTypeBuilder<Cassino> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("Cassino");
        }
    }
}