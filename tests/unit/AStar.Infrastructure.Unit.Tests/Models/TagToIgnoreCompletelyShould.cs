namespace AStar.Infrastructure.Models;

public class TagToIgnoreCompletelyShould
{
    [Fact]
    public void ReturnTheExpectedToStringOutput() => new TagToIgnoreCompletely().ToString().Should().Be(@"{""Value"":""""}");
}
