namespace AdrianRobot.Domain;

public class PointMotorYPositionUpdatedEventArgs: EventArgs
{
    public PointMotorYPositionUpdatedEventArgs(PointId pointId, int motorYPosition)
    {
        PointId = pointId ?? throw new ArgumentNullException(nameof(pointId));
        MotorYPosition = motorYPosition;
    }

    public PointId PointId { get; }
    public int MotorYPosition { get; }
}