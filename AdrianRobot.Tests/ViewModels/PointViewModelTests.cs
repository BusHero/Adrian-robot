using AdrianRobot.Domain;

namespace AdrianRobot.Tests;

public class PointViewModelTests
{
    [Fact]
    public void PointNameIsTheRightOne()
    {
        var point = new Point(new(), "Point", 100, 150);
        var programPoint = ProgramPoint.FromPoint(point);
        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoint(programPoint.PointId).Returns(point.ToOption());

        var sut = new PointViewModel(new(), programPoint, Substitute.For<IProgramsService>(), pointsService);

        sut.Name.Should().Be($"Point (y: 100, z: 150)");
    }

    [Fact]
    public void WaitIsDisplayed()
    {
        var point = new Point(new(), "Point", 100, 150);
        var programPoint = ProgramPoint.FromPoint(point) with { Wait = 30 };


        var sut = new PointViewModel(new(), programPoint, Substitute.For<IProgramsService>(), Substitute.For<IPointsService>());

        sut.Wait.Should().Be(30);
    }

    [Fact]
    public void ShakeIsDisplayed()
    {
        var point = new Point(new(), "Point", 100, 150);
        var programPoint = ProgramPoint.FromPoint(point) with { Shake = 30 };

        var sut = new PointViewModel(new(), programPoint, Substitute.For<IProgramsService>(), Substitute.For<IPointsService>());

        sut.Shake.Should().Be(30);
    }
}
