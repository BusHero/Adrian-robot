using System.Collections.Immutable;

namespace AdrianRobot;

public interface IPointsRepository
{
    void SavePoint(Point point);

    Option<Point> GetPoint(PointId point);

    bool RemovePoint(PointId pointId);

    ImmutableList<Point> GetAllPoints();
}