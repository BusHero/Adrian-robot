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
}
