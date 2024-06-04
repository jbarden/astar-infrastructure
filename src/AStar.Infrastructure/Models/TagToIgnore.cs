using System.Text.Json;

namespace AStar.Infrastructure.Models;

/// <summary>
/// The TagToIgnore class.
/// </summary>
public class TagToIgnore
{
    /// <summary>
    /// Gets or sets the value of the tag to ignore. I know, shocking...
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Returns this object in JSON format.
    /// </summary>
    /// <returns>
    /// This object serialized as a JSON object.
    /// </returns>
    public override string ToString() => JsonSerializer.Serialize(this);
}
