using System.Text.Json.Serialization;

namespace AStar.Infrastructure.Models;

/// <summary>
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SortOrder>))]
public enum SortOrder
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    SizeDescending,
    SizeAscending,
    NameDescending,
    NameAscending
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
