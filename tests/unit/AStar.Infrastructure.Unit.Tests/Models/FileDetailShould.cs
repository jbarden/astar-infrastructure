namespace AStar.Infrastructure.Models;

public class FileDetailShould
{
    [Fact]
    public void ReturnTheExpectedToStringRepresentation()
    {
        var fileDetail = new FileDetail();

        fileDetail.ToString().Should().Be(@"{""Id"":0,""FileAccessDetail"":{""Id"":0,""DetailsLastUpdated"":null,""LastViewed"":null,""SoftDeleted"":false,""SoftDeletePending"":false,""MoveRequired"":false,""HardDeletePending"":false},""FileName"":"""",""DirectoryName"":"""",""FullNameWithPath"":"""",""Height"":0,""Width"":0,""FileSize"":0,""IsImage"":false}");
    }
}
