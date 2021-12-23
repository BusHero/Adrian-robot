namespace AdrianRobot.Domain;

public record ProgramPoint(
    ProgramPointId Id, PointId PointId, int Wait = 0, int Shake = 0)
{
    public ProgramPoint(Point point) : this(new ProgramPointId(), point.Id, 0, 0) { }

    public static ProgramPoint FromPoint(Point point) => new(point);
}
