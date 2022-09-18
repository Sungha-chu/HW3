using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserAndMsg.Models
{
    public partial class TestLogContext : DbContext
    {
        public TestLogContext()
        {
        }

        public TestLogContext(DbContextOptions<TestLogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogItem> LogItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogItem>(entity =>
            {
                entity.ToTable("LogItem");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LogMsg)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.ToTable("User");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Permission)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
