﻿using System.Text.Json;

namespace AStar.Infrastructure.Models;

/// <summary>
/// </summary>
public class FileAccessDetail
{
    /// <summary>
    /// Gets or sets the Id of the <see href="FileAccessDetail"></see>. I know, shocking...
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date the file details were last updated. I know, shocking...
    /// </summary>
    public DateTime? DetailsLastUpdated { get; set; }

    /// <summary>
    /// Gets or sets the date the file was last viewed. I know, shocking...
    /// </summary>
    public DateTime? LastViewed { get; set; }

    /// <summary>
    /// Gets or sets whether the file has been 'soft deleted'. I know, shocking...
    /// </summary>
    public bool SoftDeleted { get; set; }

    /// <summary>
    /// Gets or sets whether the file has been marked as 'delete pending'. I know, shocking...
    /// </summary>
    public bool SoftDeletePending { get; set; }

    /// <summary>
    /// Gets or sets whether the file has been marked as 'needs to move'. I know, shocking...
    /// </summary>
    public bool MoveRequired { get; set; }

    /// <summary>
    /// Gets or sets whether the file has been marked as 'delete permanantely pending'. I know, shocking...
    /// </summary>
    public bool HardDeletePending { get; set; }

    /// <summary>
    /// Returns this object in JSON format.
    /// </summary>
    /// <returns>
    /// This object serialized as a JSON object.
    /// </returns>
    public override string ToString() => JsonSerializer.Serialize(this);
}
