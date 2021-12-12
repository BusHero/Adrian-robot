namespace AdrianRobot;

public class PointViewModel
{
    public PointViewModel(Point point) => Point = point ?? throw new System.ArgumentNullException(nameof(point));

    public Point Point { get; }
}
