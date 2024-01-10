using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class HistoricoSessaoMapping : IEntityTypeConfiguration<HistoricoSessao>
    {
        public void Configure(EntityTypeBuilder<HistoricoSessao> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.DataCadastro)
                .IsRequired();

            builder.HasOne(s => s.Sessao)
                .WithMany(j => j.HistoricoSessao)
                .HasForeignKey(s => s.SessaoId)
                .IsRequired(false);

            builder.ToTable("HistoricoSessao");
        }
    }
}
