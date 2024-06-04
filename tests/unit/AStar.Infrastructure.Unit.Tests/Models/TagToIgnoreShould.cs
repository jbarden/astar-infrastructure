namespace AStar.Infrastructure.Models;

public class TagToIgnoreShould
{
    [Fact]
    public void ReturnTheExpectedToStringOutput() => new TagToIgnore().ToString().Should().Be(@"{""Value"":""""}");
}
