using System.Collections.Immutable;

namespace AdrianRobot;

public interface IPointsRepository
{
    public void SavePoint(Point point);

    public Option<Point> GetPoint(PointId point);

    ImmutableList<Point> GetAllPoints();
}