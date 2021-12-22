using System.Collections.Immutable;

using AdrianRobot.Domain;

namespace AdrianRobot.Tests;

public class PointServiceTests
{
    [Fact]
    public void GetPoints()
    {
        const string pointName = "point";
        const int MotorYPosition = 0;
        const int MotorZPosition = 0;
        var point = new Point(new PointId(), pointName, MotorYPosition, MotorZPosition);
        var repository = new InMemoryPointsRepository(point);
        var pointService = new PointsService(repository);

        ImmutableList<Point> points = pointService.GetPoints();

        points.Count.Should().Be(1);
        points[0].Should().Be(point);
    }

    [Fact]
    public void CreatePoint()
    {
        var repository = new InMemoryPointsRepository();
        var pointService = new PointsService(repository);

        CheckPoint(pointService.CreatePoint(), "Point", 0, 0);

        pointService.GetPoints().Count.Should().Be(1);
        CheckPoint(pointService.GetPoints()[0], "Point", 0, 0);
    }

    [Fact]
    public void RemovePoint()
    {
        var repository = new InMemoryPointsRepository();
        repository.SavePoint(new(new(), "asd", 10, 10));

        IPointsService pointService = new PointsService(repository);
        var id = repository.GetAllPoints()[0].Id;

        pointService.RemovePoint(id);

        _ = repository.GetPoint(id).Should().Be(Option.None<Point>());
    }

    [Fact]
    public void UpdatePointName()
    {
        var repository = new InMemoryPointsRepository();
        repository.SavePoint(new(new(), "asd", 10, 10));
        var id = repository.GetAllPoints()[0].Id;

        IPointsService pointService = new PointsService(repository);

        pointService.UpdatePointName(pointID: id, pointName: "New name");
        pointService.UpdateMotorYPosition(pointID: id, motor1Position: 12);
        pointService.UpdateMotorZPosition(pointID: id, motor2Position: 13);

        _ = repository.GetPoint(id).Select(point => point.Name).Should().Be("New name".ToOption());
        _ = repository.GetPoint(id).Select(point => point.MotorYPosition).Should().Be(12.ToOption());
        _ = repository.GetPoint(id).Select(point => point.MotorZPosition).Should().Be(13.ToOption());
    }

    private static void CheckPoint(Point point, string name, int motorYPosition, int motorZPosition)
    {
        _ = point.Name.Should().Be(name);
        _ = point.MotorYPosition.Should().Be(motorYPosition);
        _ = point.MotorZPosition.Should().Be(motorZPosition);
    }
}
