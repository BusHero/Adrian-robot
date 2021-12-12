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
        Points = points.ToImmutableList();
    }

    public ProgramId Id { get; }
    public string Name { get; }
    public int Repeats { get; }
    public ImmutableList<Point> Points { get; }
}
