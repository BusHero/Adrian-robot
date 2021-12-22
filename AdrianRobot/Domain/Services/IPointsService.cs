using System.Collections.Immutable;

namespace AdrianRobot;

public interface IPointsService
{
    Point CreatePoint();
    ImmutableList<Point> GetPoints();
    Option<Point> GetPoint(PointId pointId);
    void RemovePoint(PointId id);
    void UpdatePointName(PointId pointID, string pointName);
    void UpdateMotorYPosition(PointId pointID, int motor1Position);
    void UpdateMotorZPosition(PointId pointID, int motor2Position);
}
