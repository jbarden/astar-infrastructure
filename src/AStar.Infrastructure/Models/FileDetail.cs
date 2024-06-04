using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AStar.Infrastructure.Models;

/// <summary>
/// The FileDetail class containing the current properties
/// </summary>
public class FileDetail
{
    /// <summary>
    /// The default constructor.
    /// </summary>
    public FileDetail()
    {
    }

    /// <summary>
    /// The copy constructor that allows for passing an instance of FileInfo to this class, simplifying consumer code.
    /// </summary>
    /// <param name="fileInfo">
    /// The instance of FileInfo to use.
    /// </param>
    public FileDetail(FileInfo fileInfo)
    {
        FileName = fileInfo.Name;
        DirectoryName = fileInfo.DirectoryName!;
        FileSize = fileInfo.Length;
    }

    /// <summary>
    /// Gets or sets the file name. I know, shocking...
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Returns true when the file is of a supported image type.
    /// </summary>
    [NotMapped]
    public bool IsImage => FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase)
                        || FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase)
                        || FileName.EndsWith("bmp", StringComparison.OrdinalIgnoreCase)
                        || FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase)
                        || FileName.EndsWith("jfif", StringComparison.OrdinalIgnoreCase)
                        || FileName.EndsWith("jif", StringComparison.OrdinalIgnoreCase)
                        || FileName.EndsWith("gif", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets the date the file details were last updated. I know, shocking...
    /// </summary>
    public DateTime? DetailsLastUpdated { get; set; }

    /// <summary>
    /// Gets or sets the date the file was last viewed. I know, shocking...
    /// </summary>
    public DateTime? LastViewed { get; set; }

    /// <summary>
    /// Gets or sets the name of the directory containing the file detail. I know, shocking...
    /// </summary>
    public string DirectoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the full name of the file with the path combined.
    /// </summary>
    [NotMapped]
    public string FullNameWithPath => Path.Combine(DirectoryName, FileName);

    /// <summary>
    /// Gets or sets the height of the image. I know, shocking...
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the width of the image. I know, shocking...
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the file size. I know, shocking...
    /// </summary>
    public long FileSize { get; set; }

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
    public bool NeedsToMove { get; set; }

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
