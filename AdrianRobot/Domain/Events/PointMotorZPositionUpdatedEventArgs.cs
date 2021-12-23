namespace AdrianRobot.Domain;

public class PointMotorZPositionUpdatedEventArgs : EventArgs
{
    public PointMotorZPositionUpdatedEventArgs(PointId pointId, int motorZPosition)
    {
        PointId = pointId ?? throw new ArgumentNullException(nameof(pointId));
        MotorZPosition = motorZPosition;
    }

    public PointId PointId { get; }
    public int MotorZPosition { get; }
}
