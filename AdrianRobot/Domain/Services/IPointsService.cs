using System.Collections.Immutable;

namespace AdrianRobot;

public interface IPointsService
{
    Point CreatePoint();
    ImmutableList<Point> GetPoints();
    Option<Point> GetPoint(PointId pointId);
}
