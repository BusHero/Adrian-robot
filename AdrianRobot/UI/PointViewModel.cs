using AdrianRobot.Domain;

namespace AdrianRobot;

public class PointViewModel
{
    public PointViewModel(ProgramPoint point) => Point = point ?? throw new System.ArgumentNullException(nameof(point));

    public ProgramPoint Point { get; }

    public string Name => $"{Point.Name} (y: {Point.MotorYPosition}, z: {Point.MotorZPosition})";

    public int Wait => Point.Wait;

    public int Shake => Point.Shake;
}
