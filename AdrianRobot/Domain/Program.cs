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
    public ImmutableList<ProgramPoint> Points { get; private set; }
    
    public void AddPoint(Point point, int wait, int shake)
    {
        Points = Points.Add(new ProgramPoint(point) with { Wait = wait, Shake = shake });
    }
}
