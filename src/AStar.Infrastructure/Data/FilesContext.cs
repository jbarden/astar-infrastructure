using AStar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AStar.Infrastructure.Data;

/// <summary>
/// The FileContext class.
/// </summary>
/// <remarks>
/// The list of files in the dB.
/// </remarks>
public class FilesContext(ConnectionString connectionString, AStarDbContextOptions astarDbContextOptions) : DbContext
{
    private readonly ConnectionString connectionString = connectionString;
    private readonly AStarDbContextOptions astarDbContextOptions = astarDbContextOptions;

    /// <summary>
    /// Alternative constructor
    /// </summary>
    public FilesContext() : this(new() { Value = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FilesDb" }, new())
    {
    }

    /// <summary>
    /// The list of files in the dB.
    /// </summary>
    public DbSet<FileDetail> Files { get; set; } = null!;

    /// <summary>
    /// The list of file access details in the dB.
    /// </summary>
    public DbSet<FileAccessDetail> FileAccessDetails { get; set; } = null!;

    /// <summary>
    /// The list of tags to ignore.
    /// </summary>
    public DbSet<TagToIgnore> TagsToIgnore { get; set; } = null!;

    /// <summary>
    /// The list of tags to ignore completely.
    /// </summary>
    public DbSet<TagToIgnoreCompletely> TagsToIgnoreCompletely { get; set; } = null!;

    /// <summary>
    /// The overridden OnModelCreating method.
    /// </summary>
    /// <param name="modelBuilder">
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<FileDetail>().HasKey(vf => new { vf.FileName, vf.DirectoryName });
        _ = modelBuilder.Entity<TagToIgnore>().HasKey(tag => tag.Value);
        _ = modelBuilder.Entity<TagToIgnoreCompletely>().HasKey(tag => tag.Value);
    }

    /// <summary>
    /// The overridden OnConfiguring method.
    /// </summary>
    /// <param name="optionsBuilder">
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = astarDbContextOptions.UseSqlite
                                    ? optionsBuilder.UseSqlite(connectionString.Value)
                                    : optionsBuilder.UseSqlServer(connectionString.Value,
                                                x => x.MigrationsAssembly("AStar.Infrastructure.Migrations"));

        if(astarDbContextOptions.EnableLogging)
        {
            _ = optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            _ = optionsBuilder.LogTo(Console.WriteLine);
            if(astarDbContextOptions.IncludeSensitiveData)
            {
                _ = optionsBuilder.EnableSensitiveDataLogging();
            }
        }
        else
        {
            _ = optionsBuilder.UseLoggerFactory(CreateEmptyLoggerFactory());
        }
    }

    private static ILoggerFactory CreateEmptyLoggerFactory()
                                    => LoggerFactory.Create(builder => builder.AddFilter((_, _) => false));

    private static ILoggerFactory CreateLoggerFactory()
                                    => LoggerFactory.Create(static builder => builder
                                                                    .AddFilter((category, level) => category == Command.Name && level == LogLevel.Information));
}
