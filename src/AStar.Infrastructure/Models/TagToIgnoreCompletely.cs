using System.Text.Json;

namespace AStar.Infrastructure.Models;

/// <summary>
/// The <see href="TagToIgnoreCompletely"></see> class.
/// </summary>
public class TagToIgnoreCompletely
{
    /// <summary>
    /// Gets or sets the value of the tag to ignore completely. I know, shocking...
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
