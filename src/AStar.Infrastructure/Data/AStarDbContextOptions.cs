﻿namespace AStar.Infrastructure.Data;

/// <summary>
/// The <see href="AStarDbContextOptions"></see> class is used to aid configuring the <see href="DbContext"></see>.
/// </summary>
public class AStarDbContextOptions
{
    /// <summary>
    /// Gets or sets whether logging should be enabled at all.
    /// </summary>
    public bool EnableLogging { get; set; }

    /// <summary>
    /// Gets or sets whether sensitive data should be included. If logging is not enabled, this parameter is ignored.
    /// </summary>
    public bool IncludeSensitiveData { get; set; }

    /// <summary>
    /// Gets or sets whether Sqlite should be used instead of SQLServer. The default is <c>false</c>.
    /// </summary>
    public bool UseSqlite { get; set; }
}
