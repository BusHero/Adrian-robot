namespace AdrianRobot.Domain;

public class PointNameUpdatedEventArgs : EventArgs
{
    public PointNameUpdatedEventArgs(PointId pointId, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        PointId = pointId ?? throw new ArgumentNullException(nameof(pointId));
        Name = name;
    }

    public PointId PointId { get; }
    public string Name { get; }
}