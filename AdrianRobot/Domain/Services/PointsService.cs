using System;
using System.Collections.Immutable;

namespace AdrianRobot;

public class PointsService : IPointsService
{
    private IPointsRepository PointsRepository { get; }

    public PointsService(IPointsRepository pointsRepository)
    {
        PointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
    }

    public ImmutableList<Point> GetPoints() => PointsRepository.GetAllPoints();

    public Point CreatePoint()
    {
        var point = new Point(new PointId(), "Point", 0, 0);
        PointsRepository.SavePoint(point);
        return point;
    }

    public Option<Point> GetPoint(PointId pointId) => PointsRepository.GetPoint(pointId);

    public void RemovePoint(PointId id)
    {
        ArgumentNullException.ThrowIfNull(id);

        _ = PointsRepository.RemovePoint(id);
    }

    public void UpdatePointName(PointId pointID, string pointName) => PointsRepository
        .GetPoint(pointID)
        .Modify(point => point.Name = pointName)
        .SaveToRepository(PointsRepository);

    public void UpdateMotorYPosition(PointId pointID, int motorYPosition) => PointsRepository
        .GetPoint(pointID)
        .Modify(point => point.MotorYPosition = motorYPosition)
        .SaveToRepository(PointsRepository);

    public void UpdateMotorZPosition(PointId pointID, int motorZPosition) => PointsRepository
        .GetPoint(pointID)
        .Modify(point => point.MotorZPosition = motorZPosition)
        .SaveToRepository(PointsRepository);
}

internal static class PointsRepositoryExtensions
{
    public static void SaveToRepository(this Option<Point> optionalPoint, IPointsRepository pointsRepository)
    {
        if (optionalPoint is Some<Point> { Value: var point })
        {
            pointsRepository.SavePoint(point);
        }
    }
}
