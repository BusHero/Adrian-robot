namespace AdrianRobot.Domain;

public record ProgramPoint(
    PointId Id, string Name, int MotorYPosition, int MotorZPosition, int Wait = 0, int Shake = 0)
{
    public ProgramPoint(Point point) : this(point.Id, point.Name, point.MotorYPosition, point.MotorZPosition, 0, 0) { }

    public static ProgramPoint FromPoint(Point point) => new(point);
}
