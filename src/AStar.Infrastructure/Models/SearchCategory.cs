namespace AStar.Infrastructure.Models;

/// <summary>
/// The <see href="SearchConfiguration"></see> class containing the full Scrape Configuration.
/// </summary>
public class SearchCategory
{
    /// <summary>
    /// Gets or sets the Id of the search category.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Name of the category.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Last Known Image Count.
    /// </summary>
    public int LastKnownImageCount { get; set; }

    /// <summary>
    /// Gets or sets the Last Page Visited number.
    /// </summary>
    public int LastPageVisited { get; set; }

    /// <summary>
    /// Gets or sets the Total Pages for the results.
    /// </summary>
    public int TotalPages { get; set; }
}
