using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class PlayerMapping : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("Player");
        }
    }
}