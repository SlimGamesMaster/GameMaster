﻿// <auto-generated />
using System;
using GameMasterEnterprise.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameMasterEnterprise.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    partial class MeuDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Cassino", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Cassino", (string)null);
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Jogo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Jogo", (string)null);
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CassinoId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CassinoId");

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.PlayerSaldo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("char(36)");

                    b.Property<float>("Saldo")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("PlayerSaldo", (string)null);
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Sessao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CassinoId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Dificuldade")
                        .HasColumnType("int");

                    b.Property<Guid>("JogoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("Situacao")
                        .HasColumnType("int");

                    b.Property<float?>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CassinoId");

                    b.HasIndex("JogoId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Sessao", (string)null);
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Player", b =>
                {
                    b.HasOne("GameMasterEnterprise.Domain.Models.Cassino", "Cassino")
                        .WithMany("Players")
                        .HasForeignKey("CassinoId")
                        .IsRequired();

                    b.Navigation("Cassino");
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.PlayerSaldo", b =>
                {
                    b.HasOne("GameMasterEnterprise.Domain.Models.Player", "Player")
                        .WithOne("PlayerSaldo")
                        .HasForeignKey("GameMasterEnterprise.Domain.Models.PlayerSaldo", "PlayerId")
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Sessao", b =>
                {
                    b.HasOne("GameMasterEnterprise.Domain.Models.Cassino", "Cassino")
                        .WithMany("Sessoes")
                        .HasForeignKey("CassinoId");

                    b.HasOne("GameMasterEnterprise.Domain.Models.Jogo", "Jogo")
                        .WithMany("Sessoes")
                        .HasForeignKey("JogoId");

                    b.HasOne("GameMasterEnterprise.Domain.Models.Player", "Player")
                        .WithMany("Sessoes")
                        .HasForeignKey("PlayerId");

                    b.Navigation("Cassino");

                    b.Navigation("Jogo");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Cassino", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Sessoes");
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Jogo", b =>
                {
                    b.Navigation("Sessoes");
                });

            modelBuilder.Entity("GameMasterEnterprise.Domain.Models.Player", b =>
                {
                    b.Navigation("PlayerSaldo")
                        .IsRequired();

                    b.Navigation("Sessoes");
                });
#pragma warning restore 612, 618
        }
    }
}
