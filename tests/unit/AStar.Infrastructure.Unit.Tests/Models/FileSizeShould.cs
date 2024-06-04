namespace AStar.Infrastructure.Models;

public class FileSizeShould
{
    [Fact]
    public void ReturnTheExpectedToStringOutput() => FileSize.Create(1, 2, 3).ToString().Should().Be(@"{""FileLength"":1,""Height"":2,""Width"":3}");
}
