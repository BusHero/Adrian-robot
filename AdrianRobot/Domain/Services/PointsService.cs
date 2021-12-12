using System;
using System.Collections.Immutable;

namespace AdrianRobot;

public class PointsService
{
    private IPointsRepository PointsRepository { get; }

    public PointsService(IPointsRepository pointsRepository)
    {
        PointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
    }

    public ImmutableList<Point> GetPoints() => PointsRepository.GetAllPoints();

    public void CreatePoint()
    {
        var point = new Point(new PointId(), "Point", 0, 0);
        PointsRepository.SavePoint(point);
    }
}
