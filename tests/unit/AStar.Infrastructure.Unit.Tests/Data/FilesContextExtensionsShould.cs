using AStar.Infrastructure.Fixtures;

namespace AStar.Infrastructure.Data;

public class FilesContextExtensionsShould(FilesContextFixture filesContextFixture) : IClassFixture<FilesContextFixture>
{
    private readonly FilesContext sut = filesContextFixture.SUT;

    [Fact]
    public Task ReturnFilteredFilesFromTheRootFolderOnlyWhenRecursiveIsFalse()
    {
        var response = sut.Files.FilterBySearchFolder("c:\\temp", false, CancellationToken.None);

        response.Count().Should().Be(10);

        return Verify(response);
    }

    [Fact]
    public Task ReturnFilteredFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrue()
    {
        var response = sut.Files.FilterBySearchFolder("c:\\temp", true, CancellationToken.None);

        response.Count().Should().Be(395);

        return Verify(response);
    }

    [Fact]
    public Task ReturnFilteredFilesFromTheSubFolderAndSubDirectoriesWhenRecursiveIsTrue()
    {
        var response = sut.Files.FilterBySearchFolder("c:\\temp\\1st Year Frame", true, CancellationToken.None);

        response.Count().Should().Be(18);

        return Verify(response);
    }

    [Fact]
    public Task ReturnFilteredFilesFromTheSubFolderOnlyWhenRecursiveIsFalse()
    {
        var response = sut.Files.FilterBySearchFolder("c:\\temp\\1st Year Frame", false, CancellationToken.None);

        response.Count().Should().Be(18);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreTrue()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "searchTypeNotRelevant", true, true, true, CancellationToken.None);

        response.Count().Should().Be(393);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsTrueButDeletePendingIsFalse()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "searchTypeNotRelevant", true, false, true, CancellationToken.None);

        response.Count().Should().Be(393);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsFalseButDeletePendingIsTrue()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "searchTypeNotRelevant", false, true, true, CancellationToken.None);

        response.Count().Should().Be(362);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreFalse()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "searchTypeNotRelevant", false, false, true, CancellationToken.None);

        response.Count().Should().Be(362);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreTrue_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "Images", true, true, true, CancellationToken.None);

        response.Count().Should().Be(286);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsTrueButDeletePendingIsFalse_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "Images", true, false, true, CancellationToken.None);

        response.Count().Should().Be(286);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedIsFalseButDeletePendingIsTrue_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "Images", false, true, true, CancellationToken.None);

        response.Count().Should().Be(263);

        return Verify(response);
    }

    [Fact]
    public Task ReturnMatchingFilesFromTheRootFolderAndSubDirectoriesWhenRecursiveIsTrueAndIncludeSoftDeletedAndDeletePendingAreFalse_ImagesOnly()
    {
        var response = sut.Files.GetMatchingFiles("c:\\temp", true, "Images", false, false, true, CancellationToken.None);

        response.Count().Should().Be(263);

        return Verify(response);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 364;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", true, "All", false, false, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllImageFiles()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 265;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", true, "Images", false, false, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsTopLevelFolderOnlyWhichIsEmpty()
    {
        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", false, "Images", false, false, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(0);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursively()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 87;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", true, "Images", false, false, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatCapturesAllSupportedImageTypesFromStartingSubFolder()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 4;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\temp\Famous\coats", false, "Images", false, false, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeleted()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 95;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", true, "Images", true, false, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 87;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", true, "Images", false, true, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountWhenFilterAppliedThatTargetsSpecificFolderRecursivelyButIncludeSoftDeletedAndIncludeMarkedForDeletion()
    {
        const int FilesNotSoftDeletedOrPendingDeletionCount = 95;

        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", true, "Images", true, true, false, CancellationToken.None)
                                        .Count();

        _ = matchingFilesCount.Should().Be(FilesNotSoftDeletedOrPendingDeletionCount);
    }

    [Fact]
    public void GetTheExpectedCountOfDuplicateFileGroupsWhenStartingAtTheRootFolder()
    {
        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\", true, "Duplicates", false, false, false, CancellationToken.None)
                                        .Count();

        matchingFilesCount.Should().Be(364);
    }

    [Fact]
    public void GetTheExpectedCountOfDuplicateFileGroupsWhenStartingAtSubFolder()
    {
        var matchingFilesCount = sut.Files
                                        .GetMatchingFiles(@"c:\Temp\Famous", true, "Duplicates", false, false, false, CancellationToken.None)
                                        .Count();

        matchingFilesCount.Should().Be(94);
    }
}
