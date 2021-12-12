namespace AdrianRobot.Tests;

public class PointViewModelTests
{
    [Fact]
    public void PointNameIsTheRightOne()
    {
        var point = new Point(new(), "Point", 100, 150);

        var sut = new PointViewModel(point);

        sut.Name.Should().Be($"Point (y: 100, z: 150)");
    }
}
