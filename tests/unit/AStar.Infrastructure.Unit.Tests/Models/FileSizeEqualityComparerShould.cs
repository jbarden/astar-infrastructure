namespace AStar.Infrastructure.Models;

public class FileSizeEqualityComparerShould
{
    [Fact]
    public void ReturnTrueWhenTheTwoInstancesOfFileSizeAreEquivalent()
    {
        var fileSize1 = FileSize.Create(1,2,3);
        var fileSize2 = FileSize.Create(1,2,3);
        var comparer = new FileSizeEqualityComparer();

        comparer.Equals(fileSize1, fileSize2).Should().BeTrue();
    }

    [Fact]
    public void ReturnFalseWhenTheTwoInstancesOfFileSizeAreNotEquivalent()
    {
        var fileSize1 = FileSize.Create(1,2,3);
        var fileSize2 = FileSize.Create(1,2,4);
        var comparer = new FileSizeEqualityComparer();

        comparer.Equals(fileSize1, fileSize2).Should().BeFalse();
    }
}
