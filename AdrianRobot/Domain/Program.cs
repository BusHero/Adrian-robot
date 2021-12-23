using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AdrianRobot.Domain;

public class Program : ICloneable
{
    public static Program Default { get; } = new Program(new(new Guid()), "", 0, Array.Empty<Point>());

    private readonly Dictionary<ProgramPointId, ProgramPoint> points;

    public Program(ProgramId id, string name, int repeats, IEnumerable<Point> points)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(nameof(points));

        Id = id ?? throw new System.ArgumentNullException(nameof(id));
        Name = name;
        Repeats = repeats;
        this.points = points
            .Select(ProgramPoint.FromPoint)
            .ToDictionary(point => point.Id);
    }

    public Program(Program program)
    {
        Id = program.Id;
        Name = program.Name;
        Repeats = program.Repeats;
        points = new Dictionary<ProgramPointId, ProgramPoint>(program.points);
    }

    public ProgramId Id { get; }
    public string Name { get; set; }
    public int Repeats { get; set; }
    public ImmutableList<ProgramPoint> Points => points.Values.ToImmutableList();

    public void AddPoint(Point point, int wait, int shake)
    {
        var programPoint = new ProgramPoint(new (), point.Id, wait, shake);
        points[programPoint.Id] = programPoint;
    }

    public void RemovePoint(ProgramPointId programPointId) => points.Remove(programPointId);

    public void UpdatePointWait(ProgramPointId programPointId, int wait)
    {
        if (!points.TryGetValue(programPointId, out var point))
            return;

        points[programPointId] = point with { Wait = wait };
    }

    public void UpdatePointShake(ProgramPointId programPointId, int shake)
    {
        if (!points.TryGetValue(programPointId, out var point))
            return;

        points[programPointId] = point with { Shake = shake };
    }

    public object Clone() => new Program(this);
}

