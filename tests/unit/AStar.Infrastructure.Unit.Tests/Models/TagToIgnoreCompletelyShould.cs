namespace AStar.Infrastructure.Models;

public class TagToIgnoreCompletelyShould
{
    [Fact]
    public void ReturnTheExpectedToStringOutput() => new ModelToIgnore().ToString().Should().Be(@"{""Value"":""""}");
}
