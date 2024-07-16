﻿using AStar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AStar.Infrastructure.Data;

/// <summary>
/// </summary>
public static class FilesContextExtensions
{
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
    {
        var filesToReturn = files.Include(fileDetail => fileDetail.FileAccessDetail).AsNoTracking().AsQueryable();

        if(cancellationToken.IsCancellationRequested)
        { return []; }

        filesToReturn = recursive
            ? filesToReturn.Where(file => file.DirectoryName.StartsWith(startingFolder))
            : filesToReturn.Where(file => file.DirectoryName.Equals(startingFolder));

        if(cancellationToken.IsCancellationRequested)
        { return []; }

        filesToReturn = includeSoftDeleted
            ? filesToReturn
            : filesToReturn.Where(file => !file.FileAccessDetail.SoftDeleted);

        if(cancellationToken.IsCancellationRequested)
        { return []; }

        if(!includeMarkedForDeletion)
        {
            filesToReturn = filesToReturn.Where(file => !file.FileAccessDetail.SoftDeletePending && !file.FileAccessDetail.HardDeletePending);
        }

        if(cancellationToken.IsCancellationRequested)
        { return []; }

        if(searchType == "Images")
        {
            filesToReturn = filesToReturn.Where(file => file.FileName.EndsWith("jpg")
                        || file.FileName.EndsWith("jpeg")
                        || file.FileName.EndsWith("bmp")
                        || file.FileName.EndsWith("png")
                        || file.FileName.EndsWith("jfif")
                        || file.FileName.EndsWith("jif")
                        || file.FileName.EndsWith("gif"));
        }

        if(cancellationToken.IsCancellationRequested)
        { return []; }

        if(excludeViewed)
        {
            filesToReturn = filesToReturn.Where(file => file.FileAccessDetail.LastViewed == null || file.FileAccessDetail.LastViewed <= DateTime.UtcNow.AddDays(-7));
        }

        return cancellationToken.IsCancellationRequested ? ([]) : ([.. filesToReturn]);
    }
}
