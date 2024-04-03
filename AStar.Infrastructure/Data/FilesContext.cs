using AStar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AStar.Infrastructure.Data;

/// <summary>
/// The FileContext class/
/// </summary>
public class FilesContext : DbContext
{
    /// <summary>
    /// The overriden OnConfiguring method.
    /// </summary>
    /// <param name="optionsBuilder">
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Filename=c:\\temp\\Files.db");

    /// <summary>
    /// The list of files in the dB.
    /// </summary>
    public DbSet<FileDetail> Files { get; set; } = null!;

    /// <summary>
    /// The list of tags to ignore.
    /// </summary>
    public DbSet<Tag> TagsToIgnore { get; set; } = null!;

    /// <summary>
    /// The list of tags to ignore completely.
    /// </summary>
    public DbSet<Tag> TagsToIgnoreCompletely { get; set; } = null!;

    /// <summary>
    /// The overriden OnModelCreating method.
    /// </summary>
    /// <param name="modelBuilder">
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => _ = modelBuilder.Entity<FileDetail>().HasKey(vf => new { vf.FileName, vf.DirectoryName });
}
