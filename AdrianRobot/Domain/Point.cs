namespace AdrianRobot;

public class Point
{
    public Point(PointId id, string name, int motorYPosition, int motorZPosition)
    {
        Id = id ?? throw new System.ArgumentNullException(nameof(id));
        Name = name ?? throw new System.ArgumentNullException(nameof(name));
        MotorYPosition = motorYPosition;
        MotorZPosition = motorZPosition;
    }

    public PointId Id { get; }

    public string Name { get; }

    public int MotorYPosition { get; }

    public int MotorZPosition { get; }


}
