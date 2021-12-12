namespace AdrianRobot;

public class PointViewModel
{
    public PointViewModel(Point point) => Point = point ?? throw new System.ArgumentNullException(nameof(point));

    public Point Point { get; }
    public string Name => $"{Point.Name} (y: {Point.MotorYPosition}, z: {Point.MotorZPosition})";
}
