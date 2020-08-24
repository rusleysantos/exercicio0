using System;
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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Rusley\\source\\repos\\ConsoleApp1\\ConsoleApp1\\Dados.mdf;Integrated Security=True;");
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
