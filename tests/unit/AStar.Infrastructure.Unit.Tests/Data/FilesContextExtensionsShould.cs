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

        response.Count().Should().Be(106);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreTrueAndViewedAreExcluded()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", IncludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(106);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsTrueButDeletePendingIsFalse()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", IncludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(103);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsFalseButDeletePendingIsTrue()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", ExcludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(104);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreFalse()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "searchTypeNotRelevant", ExcludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(101);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreTrue_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", IncludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(71);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsTrueButDeletePendingIsFalse_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", IncludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(68);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsFalseButDeletePendingIsTrue_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", ExcludeSoftDeleted, IncludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(69);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreFalse_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", Recursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, ExcludeViewed, CancellationToken.None);

        response.Count().Should().Be(66);

        return Verify(response);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 101;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", Recursive, "All", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllImageFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 66;

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
        const int FilesNotSoftDeletedOrPendingDeletionCount = 24;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\temp\wwwroot - Copy\AI", Recursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllSupportedImageTypesFromStartingSubFolder()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 17;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\temp\wwwroot - Copy\AI", NotRecursive, "Images", ExcludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeleted()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 32;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\temp\wwwroot - Copy", Recursive, "Images", IncludeSoftDeleted, ExcludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 33;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\temp\wwwroot - Copy", Recursive, "Images", ExcludeSoftDeleted, IncludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeletedAndIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 35;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\temp\wwwroot - Copy", Recursive, "Images", IncludeSoftDeleted, IncludeMarkedForDeletion, !ExcludeViewed, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }
}
