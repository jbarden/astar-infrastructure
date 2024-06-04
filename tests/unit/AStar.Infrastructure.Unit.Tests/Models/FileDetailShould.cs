namespace AStar.Infrastructure.Models;

public class FileDetailShould
{
    [Fact]
    public void ReturnTheExpectedToStringRepresentation()
    {
        var fileDetail = new FileDetail();

        fileDetail.ToString().Should().Be(@"{""FileName"":"""",""IsImage"":false,""DetailsLastUpdated"":null,""LastViewed"":null,""DirectoryName"":"""",""FullNameWithPath"":"""",""Height"":0,""Width"":0,""FileSize"":0,""SoftDeleted"":false,""SoftDeletePending"":false,""NeedsToMove"":false,""HardDeletePending"":false}");
    }

    [Theory]
    [InlineData("jpg", true)]
    [InlineData("jpeg", true)]
    [InlineData("bmp", true)]
    [InlineData("jif", true)]
    [InlineData("png", true)]
    [InlineData("jfif", true)]
    [InlineData("gif", true)]
    [InlineData("tst", false)]
    [InlineData("txt", false)]
    [InlineData("doc", false)]
    [InlineData("JPG", true)]
    [InlineData("JPEG", true)]
    [InlineData("BMP", true)]
    [InlineData("JIF", true)]
    [InlineData("PNG", true)]
    [InlineData("JFIF", true)]
    [InlineData("GIF", true)]
    [InlineData("TST", false)]
    [InlineData("TXT", false)]
    [InlineData("DOC", false)]
    public void ReturnTheExpectedValueForIsImageIgnoringCase(string extension, bool expectedResponse)
    {
        var fileDetail = new FileDetail(){FileName = $"DoesNotMatter.{extension}"};

        fileDetail.IsImage.Should().Be(expectedResponse);
    }
}
