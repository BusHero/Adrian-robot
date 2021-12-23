namespace AdrianRobot;

public class Point
{
    public static readonly Point Empty = new(PointId.Empty, "", 0, 0);

    public Point(PointId id, string name, int motorYPosition, int motorZPosition)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        MotorYPosition = motorYPosition;
        MotorZPosition = motorZPosition;
    }

    public PointId Id { get; }

    public string Name { get; set; }

    public int MotorYPosition { get; set; }

    public int MotorZPosition { get; set; }
}
