using AStar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using static AStar.Utilities.StringExtensions;

namespace AStar.Infrastructure.Data;

/// <summary>
/// </summary>
public static class FilesContextExtensions
{
    /// <summary>
    /// The FilterBySearchFolder method will return the files matching the specified criteria.
    /// </summary>
    /// <param name="files">
    /// The list of files to filter.
    /// </param>
    /// <param name="startingFolder">
    /// The starting folder for the filter to be applied from.
    /// </param>
    /// <param name="recursive">
    /// A boolean to control whether the filter is applied recursively or not.
    /// </param>
    /// <param name="cancellationToken">
    /// An instance of <see href="CancellationToken"></see> to cancel the filter when requested.
    /// </param>
    /// <returns>
    /// The original list of files for further filtering.
    /// </returns>
    public static IEnumerable<FileDetail> FilterBySearchFolder(this DbSet<FileDetail> files, string startingFolder, bool recursive, CancellationToken cancellationToken)
            => cancellationToken.IsCancellationRequested
                                    ? files
                                    : FilterBySearchFolder(files, startingFolder, recursive);

    /// <summary>
    /// </summary>
    /// <param name="files">
    /// The list of files to filter.
    /// </param>
    /// <param name="startingFolder">
    /// The starting folder for the filter to be applied from.
    /// </param>
    /// <param name="recursive">
    /// A boolean to control whether the filter is applied recursively or not.
    /// </param>
    /// <param name="searchType">
    /// A string representation of the required Search Type.
    /// </param>
    /// <param name="includeSoftDeleted">
    /// A boolean to control whether the filter includes files marked as Soft Deleted or not.
    /// </param>
    /// <param name="includeMarkedForDeletion">
    /// A boolean to control whether the filter includes files marked Marked for Deletion or not.
    /// </param>
    /// <param name="excludeViewed">
    /// A boolean to control whether the filter includes files recently viewed or not.
    /// </param>
    /// <param name="cancellationToken">
    /// An instance of <see href="CancellationToken"></see> to cancel the filter when requested.
    /// </param>
    /// <returns>
    /// The original list of files for further filtering.
    /// </returns>
    public static IEnumerable<FileDetail> GetMatchingFiles(this DbSet<FileDetail> files, string startingFolder, bool recursive, string searchType, bool includeSoftDeleted, bool includeMarkedForDeletion, bool excludeViewed, CancellationToken cancellationToken)
                                                => files
                                                        .FilterBySearchFolder(startingFolder, recursive, cancellationToken)
                                                        .FilterSoftDeleted(includeSoftDeleted, cancellationToken)
                                                        .FilterMarkedForDeletion(includeMarkedForDeletion, cancellationToken)
                                                        .FilterImagesIfApplicable(searchType, cancellationToken)
                                                        .FilterRecentlyViewed(excludeViewed, cancellationToken).ToList();

    private static IEnumerable<FileDetail> FilterBySearchFolder(DbSet<FileDetail> files, string startingFolder, bool recursive) => startingFolder.IsNullOrWhiteSpace()
                                                ? []
                                            : GetFiles(files, startingFolder, recursive);

    private static IEnumerable<FileDetail> GetFiles(DbSet<FileDetail> files, string startingFolder, bool recursive)
        => recursive
                ? files.Where(file => file.DirectoryName.StartsWith(startingFolder))
                : (IEnumerable<FileDetail>)files.Where(file => file.DirectoryName.Equals(startingFolder));

    private static IEnumerable<FileDetail> FilterSoftDeleted(this IEnumerable<FileDetail> files, bool includeSoftDeleted, CancellationToken cancellationToken)
                => cancellationToken.IsCancellationRequested
                                    ? files
                                    : FilterSoftDeleted(files, includeSoftDeleted);

    private static IEnumerable<FileDetail> FilterSoftDeleted(IEnumerable<FileDetail> files, bool includeSoftDeleted) => !includeSoftDeleted
                ? files.Where(file => !file.SoftDeleted) : files;

    private static IEnumerable<FileDetail> FilterMarkedForDeletion(this IEnumerable<FileDetail> files, bool includeMarkedForDeletion, CancellationToken cancellationToken)
                => cancellationToken.IsCancellationRequested
                                    ? files
                                    : FilterMarkedForDeletion(files, includeMarkedForDeletion);

    private static IEnumerable<FileDetail> FilterMarkedForDeletion(IEnumerable<FileDetail> files, bool includeMarkedForDeletion)
                        => !includeMarkedForDeletion
                                    ? files.Where(file => !file.SoftDeletePending && !file.HardDeletePending)
                                    : files;
}
