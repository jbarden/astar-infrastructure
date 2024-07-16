namespace AStar.Infrastructure.Models;

/// <summary>
/// The <see href="ScrapeConfiguration"></see> class containing the full Scrape Configuration.
/// </summary>
public class ScrapeConfiguration
{
    /// <summary>
    /// Gets or sets the Id of the configuration.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The <see href="UserConfiguration"></see> class containing the User Configuration.
    /// </summary>
    public UserConfiguration UserConfiguration { get; set; } = new();

    /// <summary>
    /// The <see href="SearchConfiguration"></see> class containing the Search Configuration.
    /// </summary>
    public SearchConfiguration SearchConfiguration { get; set; } = new();

    /// <summary>
    /// The <see href="ScrapeDirectories"></see> class containing the Directory Configuration.
    /// </summary>
    public ScrapeDirectories ScrapeDirectories { get; set; } = new();
}
