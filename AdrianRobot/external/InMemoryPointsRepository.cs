using System.Collections.Immutable;

namespace AdrianRobot.Domain;

public class InMemoryPointsRepository : IPointsRepository
{
    private readonly Dictionary<PointId, Point> points;

    public InMemoryPointsRepository(IEnumerable<Point> points) => this.points = points
        .ToDictionary(point => point.Id);

    public InMemoryPointsRepository(params Point[] points) : this(points.AsEnumerable()) { }

    public InMemoryPointsRepository() : this(Array.Empty<Point>()) { }

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