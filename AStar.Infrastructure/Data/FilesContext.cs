using AStar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AStar.Infrastructure.Data;

public class FilesContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Filename=c:\\temp\\Files.db");

    public DbSet<FileDetail> Files { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => _ = modelBuilder.Entity<FileDetail>().HasKey(vf => new { vf.FileName, vf.DirectoryName });
}
