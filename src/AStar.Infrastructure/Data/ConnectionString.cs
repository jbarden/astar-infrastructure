namespace AStar.Infrastructure.Data;

/// <summary>
/// The <see href="ConnectionString"></see> class for use when configuring the context.
/// </summary>
public class ConnectionString
{
    /// <summary>
    /// Gets or sets the actual connection string.
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
