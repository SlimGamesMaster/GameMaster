using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class CassinoMapping : IEntityTypeConfiguration<Cassino>
    {
        public void Configure(EntityTypeBuilder<Cassino> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Players)
                    .WithOne(p => p.Cassino)
                    .HasForeignKey(p => p.CassinoId)
                    .OnDelete(DeleteBehavior.SetNull);


            builder.ToTable("Cassino");
        }
    }
}
