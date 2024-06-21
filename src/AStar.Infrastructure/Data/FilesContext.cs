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
public class FilesContext : DbContext
{
    private readonly ConnectionString connectionString;
    private readonly AStarDbContextOptions astarDbContextOptions;

    /// <summary>
    /// Alternative constructor used when creating migrations, the connection string is hard-coded.
    /// </summary>
    public FilesContext() : this(new ConnectionString() { Value = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FilesDb" }, new())
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="connectionString">
    /// </param>
    /// <param name="astarDbContextOptions">
    /// </param>
    public FilesContext(ConnectionString connectionString, AStarDbContextOptions astarDbContextOptions)
    {
        this.connectionString = connectionString;
        this.astarDbContextOptions = astarDbContextOptions;
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
    /// The list of models to ignore completely.
    /// </summary>
    public DbSet<ModelToIgnore> ModelsToIgnore { get; set; } = null!;

    /// <summary>
    /// The Scrape Configuration settings.
    /// </summary>
    public DbSet<ScrapeConfiguration> ScrapeConfiguration { get; set; } = null!;

    /// <summary>
    /// The overridden OnModelCreating method.
    /// </summary>
    /// <param name="modelBuilder">
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
        _ = modelBuilder.Entity<FileDetail>().HasKey(fileDetail => fileDetail.Id);
        _ = modelBuilder.Entity<FileAccessDetail>().HasKey(fileAccessDetail => fileAccessDetail.Id);
        _ = modelBuilder.Entity<ScrapeConfiguration>().HasKey(scrapeConfiguration => scrapeConfiguration.Id);
        _ = modelBuilder.Entity<TagToIgnore>().HasKey(tag => tag.Value);
        _ = modelBuilder.Entity<ModelToIgnore>().HasKey(tag => tag.Value);
    }

    /// <summary>
    /// The overridden OnConfiguring method.
    /// </summary>
    /// <param name="optionsBuilder">
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = astarDbContextOptions.InMemory
                                    ? optionsBuilder.UseInMemoryDatabase(databaseName: $"FilesTestDb{Guid.NewGuid()}")
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
