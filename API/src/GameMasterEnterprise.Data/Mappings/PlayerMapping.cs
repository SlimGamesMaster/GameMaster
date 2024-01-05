﻿using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMasterEnterprise.Data.Mappings
{
    public class PlayerMapping : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Cassino)
                    .WithMany(c => c.Players)
                    .HasForeignKey(p => p.CassinoId)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Player");
        }
    }
}