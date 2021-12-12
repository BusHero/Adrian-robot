using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AdrianRobot.Domain;

public class Program
{
    public Program(ProgramId id, string name, int repeats, IEnumerable<Point> points)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(nameof(points));

        Id = id ?? throw new System.ArgumentNullException(nameof(id));
        Name = name;
        Repeats = repeats;
        Points = points
            .Select(ProgramPoint.FromPoint)
            .ToImmutableList();
    }

    public ProgramId Id { get; }
    public string Name { get; set; }
    public int Repeats { get; }
    public ImmutableList<ProgramPoint> Points { get; }
}

public record ProgramPoint(
    PointId Id, string Name, int MotorYPosition, int MotorZPosition, int Wait = 0, int Shake = 0)
{
    public ProgramPoint(Point point) : this(point.Id, point.Name, point.MotorYPosition, point.MotorZPosition, 0, 0) { }

    public static ProgramPoint FromPoint(Point point) => new(point);
}
