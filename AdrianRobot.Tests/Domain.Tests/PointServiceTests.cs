using AdrianRobot.Domain;

using System.Collections.Immutable;

namespace AdrianRobot.Tests;

public class PointServiceTests
{
    [Fact]
    public void GetPoints()
    {
        var repository = new InMemoryPointRepository(new Point(new PointId()));
        var pointService = new PointsService(repository);

        ImmutableList<Point> points = pointService.GetPoints();

        points.Count.Should().Be(1);
    }
}

public class InMemoryPointRepository : IPointsRepository
{
    private Dictionary<PointId, Point> points;

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