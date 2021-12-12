using AdrianRobot.Domain;

using System.Collections.Immutable;

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
        var repository = new InMemoryPointRepository(point);
        var pointService = new PointsService(repository);

        ImmutableList<Point> points = pointService.GetPoints();

        points.Count.Should().Be(1);
        points[0].Should().Be(point);
    }

    [Fact]
    public void CreatePoint()
    {
        var repository = new InMemoryPointRepository();
        var pointService = new PointsService(repository);

        pointService.CreatePoint();
        
        pointService.GetPoints().Count.Should().Be(1);
        var point = pointService.GetPoints()[0];
        point.Name.Should().Be("Point");
        point.MotorYPosition.Should().Be(0);
        point.MotorZPosition.Should().Be(0);
    }
}

public class InMemoryPointRepository : IPointsRepository
{
    private readonly Dictionary<PointId, Point> points;

    public InMemoryPointRepository(IEnumerable<Point> points) => this.points = points
        .ToDictionary(point => point.Id);

    public InMemoryPointRepository(params Point[] points): this(points.AsEnumerable()) { }

    public InMemoryPointRepository() : this(Array.Empty<Point>()) { }

    public ImmutableList<Point> GetAllPoints() => points.Values.ToImmutableList();

    public Option<Point> GetPoint(PointId point) => points.TryGetValue(point, out var pointValue) switch
    {
        true => pointValue.ToOption(),
        _ => Option.None<Point>()
    };

    public void SavePoint(Point point)
    {
        ArgumentNullException.ThrowIfNull(point);

        points[point.Id] = point;
    }
}