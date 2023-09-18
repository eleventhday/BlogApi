using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models;

public partial class BlogDbContext : DbContext
{
    public BlogDbContext()
    {
    }

    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=S001001DB10;Database=BlogDb;Trusted_Connection=True;TrustServerCertificate=True ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.HasIndex(e => e.AuthorId, "IX_Blog_AuthorId");

            entity.HasIndex(e => e.CategoryId, "IX_Blog_CategoryId");

            entity.Property(e => e.CreateTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs).HasForeignKey(d => d.AuthorId);

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
