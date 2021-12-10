using System;
using System.Collections.Generic;

namespace AdrianRobot.Domain;

public class ProgramId : IEquatable<ProgramId?>
{
    private readonly Guid id;

    public ProgramId() : this(Guid.NewGuid()) { }

    public ProgramId(Guid id) => this.id = id;

    public override bool Equals(object? obj) => Equals(obj as ProgramId);

    public bool Equals(ProgramId? other) => other != null &&
               id.Equals(other.id);

    public override int GetHashCode() => HashCode.Combine(id);

    public static bool operator ==(ProgramId? left, ProgramId? right) => EqualityComparer<ProgramId>.Default.Equals(left, right);

    public static bool operator !=(ProgramId? left, ProgramId? right) => !(left == right);
}
