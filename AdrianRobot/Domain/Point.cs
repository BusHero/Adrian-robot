namespace AdrianRobot;

public class Point: ICloneable
{
    public static readonly Point Empty = new(PointId.Empty, "", 0, 0);

    public Point(PointId id, string name, int motorYPosition, int motorZPosition)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        MotorYPosition = motorYPosition;
        MotorZPosition = motorZPosition;
    }

    public Point(Point point)
    {
        Id = point.Id;
        Name = point.Name;
        MotorYPosition = point.MotorYPosition;
        MotorZPosition = point.MotorZPosition;
    }

    public PointId Id { get; }

    public string Name { get; set; }

    public int MotorYPosition { get; set; }

    public int MotorZPosition { get; set; }

    public object Clone() => new Point(Id, Name, MotorYPosition, MotorZPosition);
}
