using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Always.Encrypted.Web.Models
{
    public partial class Always_Encrypted_DBContext : DbContext
    {
        public Always_Encrypted_DBContext()
        {
        }

        public Always_Encrypted_DBContext(DbContextOptions<Always_Encrypted_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientInfo> ClientInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("<Hidden>");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientInfo>(entity =>
            {
                entity.ToTable("Client_Info");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasColumnName("Client_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.VisaNo)
                    .IsRequired()
                    .HasColumnName("Visa_No")
                    .HasMaxLength(16);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
