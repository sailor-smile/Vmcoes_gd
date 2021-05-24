using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Vmcoes_gd.Enties
{
    public partial class VmcoesContext : DbContext
    {
        public VmcoesContext()
        {
        }

        public VmcoesContext(DbContextOptions<VmcoesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConstructionSite> ConstructionSite { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VmcoesGd> VmcoesGd { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConstructionSite>(entity =>
            {
                entity.HasKey(e => e.ConstCode);

                entity.ToTable("Construction_site");

                entity.Property(e => e.ConstCode)
                    .HasColumnName("const_code")
                    .HasMaxLength(10)
                     .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ConstAddr)
                    .HasColumnName("const_addr")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ConstName)
                    .HasColumnName("const_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SortNo).HasColumnName("sort_no");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UId);

                entity.Property(e => e.UId)
                    .HasColumnName("u_id")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActualName)
                    .HasColumnName("actual_name")
                    .HasMaxLength(10);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UName)
                    .HasColumnName("u_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UPassword)
                    .HasColumnName("u_password")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VmcoesGd>(entity =>
            {
                entity.ToTable("Vmcoes_gd");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Airtight)
                    .HasColumnName("airtight")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Certificate)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ConstAddr)
                    .HasColumnName("const_addr")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ConstCode)
                    .HasColumnName("const_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ConstName)
                    .HasColumnName("const_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InputRiqi)
                    .HasColumnName("input_riqi")
                    .HasColumnType("datetime");

                entity.Property(e => e.InputTime)
                    .HasColumnName("input_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Rinse)
                    .HasColumnName("rinse")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SortNo).HasColumnName("sort_no");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Trans)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TransportcompanyName)
                    .HasColumnName("transportcompany_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
