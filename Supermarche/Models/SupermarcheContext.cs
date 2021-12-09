using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Supermarche.Models
{
    public partial class SupermarcheContext : DbContext
    {
        public SupermarcheContext()
        {
        }

        public SupermarcheContext(DbContextOptions<SupermarcheContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<CategorieArticle> CategorieArticle { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Panier> Panier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prix)
                    .HasColumnName("prix")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Quantite).HasColumnName("quantite");

                entity.HasOne(d => d.IdCategorieNavigation)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.IdCategorie)
                    .HasConstraintName("FK_categorie_article");
            });

            modelBuilder.Entity<CategorieArticle>(entity =>
            {
                entity.ToTable("categorie_article");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Label)
                    .HasColumnName("label")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Argent)
                    .HasColumnName("argent")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasColumnName("prenom")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Panier>(entity =>
            {
                entity.HasKey(e => new { e.IdPanier, e.IdClient, e.IdArticle })
                    .HasName("PK__panier__A8DE5E119D26C42D");

                entity.ToTable("panier");

                entity.Property(e => e.IdPanier).HasColumnName("id_panier");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.IdArticle).HasColumnName("id_article");

                entity.Property(e => e.DateAchat)
                    .HasColumnName("date_achat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Quantite).HasColumnName("quantite");

                entity.HasOne(d => d.IdArticleNavigation)
                    .WithMany(p => p.Panier)
                    .HasForeignKey(d => d.IdArticle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_panier_article");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Panier)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_panier_client");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
