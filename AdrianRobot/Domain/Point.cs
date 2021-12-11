namespace AdrianRobot;

public class Point
{
    public Point(PointId id) => Id = id ?? throw new System.ArgumentNullException(nameof(id));

    public PointId Id { get; }
}
