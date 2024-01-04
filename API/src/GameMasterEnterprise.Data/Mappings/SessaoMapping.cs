using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class SessaoMapping : IEntityTypeConfiguration<Sessao>
    {
        public void Configure(EntityTypeBuilder<Sessao> builder)
        {
            // Define a chave primária da entidade
            builder.HasKey(s => s.Id);

            // Configuração da propriedade DataCadastro (assumindo que seja do tipo DateTime)
            builder.Property(s => s.DataCadastro)
                .IsRequired();

            // Mapeamento da relação 1:N com Jogo (opcional)
            builder.HasOne(s => s.Jogo)
                .WithMany(j => j.Sessoes)
                .HasForeignKey(s => s.JogoId)
                .IsRequired(false); // A relação é opcional (1:N)

            // Mapeamento da relação 1:N com Player (opcional)
            builder.HasOne(s => s.Player)
                .WithMany(p => p.Sessoes)
                .HasForeignKey(s => s.PlayerId)
                .IsRequired(false); // A relação é opcional (1:N)

            // Mapeamento da relação 1:N com Cassino (opcional)
            builder.HasOne(s => s.Cassino)
                .WithMany(c => c.Sessoes)
                .HasForeignKey(s => s.CassinoId)
                .IsRequired(false); // A relação é opcional (1:N)

            builder.ToTable("Sessao");
        }
    }
}
