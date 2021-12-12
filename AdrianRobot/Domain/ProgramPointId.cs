namespace AdrianRobot.Domain;

public class ProgramPointId : IEquatable<ProgramPointId?>
{
    private readonly Guid id;

    public ProgramPointId(): this(Guid.NewGuid()) { }

    public ProgramPointId(Guid id) => this.id = id;

    public override bool Equals(object? obj) => Equals(obj as ProgramPointId);

    public bool Equals(ProgramPointId? other) => other != null &&
               id.Equals(other.id);

    public override int GetHashCode() => HashCode.Combine(id);

    public static bool operator ==(ProgramPointId? left, ProgramPointId? right) => EqualityComparer<ProgramPointId>.Default.Equals(left, right);

    public static bool operator !=(ProgramPointId? left, ProgramPointId? right) => !(left == right);
}