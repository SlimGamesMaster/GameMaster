using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class JogoMapping : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("Jogo");
        }
    }
}