namespace AdrianRobot;

public class PossiblePointViewModel
{
    public PossiblePointViewModel(Point point)
    {
        Point = point ?? throw new ArgumentNullException(nameof(point));
    }

    public string Name => $"{Point.Name}(y: {Point.MotorYPosition}, z: {Point.MotorZPosition})";

    public Point Point { get; }
}
