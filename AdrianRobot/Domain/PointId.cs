using System;
using System.Collections.Generic;

namespace AdrianRobot;

public class PointId : IEquatable<PointId?>
{
    public PointId(): this(Guid.NewGuid()) { }

    public PointId(Guid id) => Id = id;

    private Guid Id { get; }

    public override bool Equals(object? obj) => Equals(obj as PointId);

    public bool Equals(PointId? other) => other != null &&
               Id.Equals(other.Id);

    public override int GetHashCode() => HashCode.Combine(Id);

    public static bool operator ==(PointId? left, PointId? right) => EqualityComparer<PointId>.Default.Equals(left, right);

    public static bool operator !=(PointId? left, PointId? right) => !(left == right);
}
