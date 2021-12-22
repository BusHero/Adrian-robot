namespace AdrianRobot;

public class InMemorySettingsRepository : ISettingsRepository
{
    public int Motor1Speed { get; set; }
    public int Motor2Speed { get; set; }
}
