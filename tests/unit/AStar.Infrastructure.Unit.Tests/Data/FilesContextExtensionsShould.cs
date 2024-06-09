using AStar.Infrastructure.Fixtures;

namespace AStar.Infrastructure.Data;

public class FilesContextExtensionsShould(FilesContextFixture filesContextFixture) : IClassFixture<FilesContextFixture>
{
    private readonly FilesContext sut = filesContextFixture.SUT;
    private const bool Recursive = true;
    private const bool NotRecursive = false;
    private const bool IncludeSoftDeleted = true;
    private const bool ExcludeSoftDeleted = false;
    private const bool IncludeMarkedForDeletion = true;
    private const bool ExcludeMarkedForDeletion = false;
    private const bool ExcludeViewed = true;

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreTrueAndViewedAreNotExcluded()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", IncludeSoftDeleted, IncludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(400);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreTrueAndViewedAreExcluded()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", IncludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(341);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsTrueButDeletePendingIsFalse()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", IncludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(239);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsFalseButDeletePendingIsTrue()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", ExcludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(303);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreFalse()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", ExcludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(212);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreTrue_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", IncludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(249);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsTrueButDeletePendingIsFalse_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", IncludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(173);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsFalseButDeletePendingIsTrue_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", ExcludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(219);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreFalse_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(152);

        return Verify(response);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 248;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", Recursive, "All", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllImageFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 179;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", Recursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsTopLevelFolderOnlyWhichIsEmpty()
    {
        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", NotRecursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(0);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursively()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 57;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", Recursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllSupportedImageTypesFromStartingSubFolder()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 2;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\temp\Famous\coats", NotRecursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeleted()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 67;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", Recursive, "Images", IncludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 83;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", Recursive, "Images", ExcludeSoftDeleted, IncludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeletedAndIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 95;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", Recursive, "Images", IncludeSoftDeleted, IncludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountOfDuplicateFileGroupsWhenStartingAtTheRootFolder()
    {
        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", Recursive, "Duplicates", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        matchingFilesCount.Should().Be(248);
    }

    [Fact]
    public void GetTheExpectedCountOfDuplicateFileGroupsWhenStartingAtSubFolder()
    {
        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", Recursive, "Duplicates", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        matchingFilesCount.Should().Be(62);
    }
}
