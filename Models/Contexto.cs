﻿using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConsoleApp1.Models
{
    public partial class Contexto : DbContext
    {
        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public virtual DbSet<CarinhoCompra> CarinhoCompra { get; set; }
        public virtual DbSet<ItemCompra> ItemCompra { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var caminho = Directory.GetCurrentDirectory().Split("\\");
            StringBuilder caminhoFinal = new StringBuilder();

            foreach (var trecho in caminho)
            {
                if (trecho != "exercicio0")
                {
                    caminhoFinal.Append($"{trecho}\\");
                }
                else
                {
                    break;
                }
            }

            string cadeiaConexao = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={caminhoFinal}exercicio0\\Dados.mdf;Integrated Security=True";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(cadeiaConexao);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarinhoCompra>(entity =>
            {
                entity.HasKey(e => e.IdCarinhoCompra)
                    .HasName("PK__tmp_ms_x__12AA5C4E5A7ACA61");

                entity.Property(e => e.IdCarinhoCompra).HasColumnName("idCarinhoCompra");

                entity.Property(e => e.ValorTotal)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemCompra>(entity =>
            {
                entity.HasKey(e => e.IdItemCompra)
                    .HasName("PK__tmp_ms_x__26A13E23B0D876E2");

                entity.Property(e => e.IdItemCompra).HasColumnName("idItemCompra");

                entity.Property(e => e.IdCarinhoCompra).HasColumnName("idCarinhoCompra");

                entity.Property(e => e.IdProduto).HasColumnName("idProduto");

                entity.HasOne(d => d.IdCarinhoCompraNavigation)
                    .WithMany(p => p.ItemCompra)
                    .HasForeignKey(d => d.IdCarinhoCompra)
                    .HasConstraintName("FK_Produto_ToTable1");

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.ItemCompra)
                    .HasForeignKey(d => d.IdProduto)
                    .HasConstraintName("FK_Produto_ToTable");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__tmp_ms_x__5EEDF7C3172462FE");

                entity.Property(e => e.IdProduto).HasColumnName("idProduto");

                entity.Property(e => e.Nome)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
