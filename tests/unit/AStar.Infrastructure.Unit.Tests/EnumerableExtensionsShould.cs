using AStar.Infrastructure.Data;
using AStar.Infrastructure.Fixtures;
using AStar.Infrastructure.Models;

namespace AStar.Infrastructure;

public class EnumerableExtensionsShould(FilesContextFixture filesContextFixture) : IClassFixture<FilesContextFixture>
{
    private readonly FilesContext sut = filesContextFixture.SUT;

    [Fact]
    public Task ReturnCorrectCountWhenFilteringImages()
    {
        var response = sut.Files.FilterImagesIfApplicable("Images", CancellationToken.None);

        response.Count().Should().Be(239);

        return Verify(response);
    }

    [Fact]
    public Task ReturnCorrectCountWhenAllFileTypesSpecified()
    {
        var response = sut.Files.FilterImagesIfApplicable("All", CancellationToken.None);

        response.Count().Should().Be(400);

        return Verify(response);
    }

    [Fact]
    public Task ReturnCorrectCountWhenDuplicatesSpecified()
    {
        var response = sut.Files.FilterImagesIfApplicable("Duplicates", CancellationToken.None);

        response.Count().Should().Be(400);

        return Verify(response);
    }

    [Fact]
    public Task ReturnTheExpectedFilesListByNameAscending()
    {
        var response = sut.Files.OrderFiles(SortOrder.NameAscending);

        return Verify(response);
    }

    [Fact]
    public Task ReturnTheExpectedFilesListByNameDescending()
    {
        var response = sut.Files.OrderFiles(SortOrder.NameDescending);

        return Verify(response);
    }

    [Fact]
    public Task ReturnTheExpectedFilesListBySizeAscending()
    {
        var response = sut.Files.OrderFiles(SortOrder.SizeAscending);

        return Verify(response);
    }

    [Fact]
    public Task ReturnTheExpectedFilesListBySizeDescending()
    {
        var response = sut.Files.OrderFiles(SortOrder.SizeDescending);

        return Verify(response);
    }

    [Fact]
    public void ReturnTheCorrectDuplicatesCount()
    {
        var response = sut.Files.GetDuplicatesCount(CancellationToken.None);

        response.Should().Be(40);
    }

    [Fact]
    public Task ReturnTheCorrectDuplicates()
    {
        var response = sut.Files.GetDuplicates();

        return Verify(response);
    }
}
