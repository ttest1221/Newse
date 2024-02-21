using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Newse2.Models
{
    public partial class NewseDBContext : DbContext
    {
        public NewseDBContext()
        {
        }

        public NewseDBContext(DbContextOptions<NewseDBContext> options)
            : base(options)
        {
        }

        private static NewseDBContext _context;
        public static NewseDBContext GetContext()
        {
            if (_context == null)
                _context = new NewseDBContext();
            return _context;
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Commentary> Commentaries { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-A3GAUE6\\SQLEXPRESS;Database=NewseDB;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
            });

            modelBuilder.Entity<Commentary>(entity =>
            {
                entity.ToTable("Commentary");

                entity.Property(e => e.DateOfWritten).HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Commentaries)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Commentary_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Commentaries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Commentary_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.DateOfCreation).HasColumnType("datetime");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Post_User");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Post_Category");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.DateOfRegistration).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
