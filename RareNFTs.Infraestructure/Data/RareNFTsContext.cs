using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RareNFTs.Infraestructure.Models;

namespace RareNFTs.Infraestructure.Data;

public partial class RareNFTsContext : DbContext
{
    public RareNFTsContext(DbContextOptions<RareNFTsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Card { get; set; }

    public virtual DbSet<Client> Client { get; set; }

    public virtual DbSet<ClientNft> ClientNft { get; set; }

    public virtual DbSet<Country> Country { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }

    public virtual DbSet<InvoiceHeader> InvoiceHeader { get; set; }

    public virtual DbSet<InvoiceStatus> InvoiceStatus { get; set; }

    public virtual DbSet<Nft> Nft { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Wallet> Wallet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdCountry)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.IdUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdWallet)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_User");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Client)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK_Client_Country");

            entity.HasOne(d => d.IdWalletNavigation).WithMany(p => p.Client)
                .HasForeignKey(d => d.IdWallet)
                .HasConstraintName("FK_Client_Wallet");
        });

        modelBuilder.Entity<ClientNft>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdNft });

            entity.ToTable("ClientNFT");

            entity.Property(e => e.IdClient)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdNft)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IdNFT");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientNft)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientNFT_Client");

            entity.HasOne(d => d.IdNftNavigation).WithMany(p => p.ClientNft)
                .HasForeignKey(d => d.IdNft)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientNFT_NFT");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => new { e.IdInvoice, e.IdNft });

            entity.Property(e => e.IdInvoice)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdNft)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IdNFT");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Tax).HasColumnType("money");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.InvoiceDetail)
                .HasForeignKey(d => d.IdInvoice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_InvoiceHeader");

            entity.HasOne(d => d.IdNftNavigation).WithMany(p => p.InvoiceDetail)
                .HasForeignKey(d => d.IdNft)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_NFT");
        });

        modelBuilder.Entity<InvoiceHeader>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IdCard)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdClient)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdCardNavigation).WithMany(p => p.InvoiceHeader)
                .HasForeignKey(d => d.IdCard)
                .HasConstraintName("FK_InvoiceHeader_Card");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.InvoiceHeader)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_InvoiceHeader_Client");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.InvoiceHeader)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_InvoiceHeader_InvoiceStatus");
        });

        modelBuilder.Entity<InvoiceStatus>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Nft>(entity =>
        {
            entity.ToTable("NFT");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdRole)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.User)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Purse).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
