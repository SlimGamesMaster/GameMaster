using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class PlayerSaldoMapping : IEntityTypeConfiguration<PlayerSaldo>
    {
        public void Configure(EntityTypeBuilder<PlayerSaldo> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.HasOne(ps => ps.Player)
                .WithOne(p => p.PlayerSaldo)
                .HasForeignKey<PlayerSaldo>(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PlayerSaldo");
        }
    }
}