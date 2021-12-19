namespace AdrianRobot;

public class SettingsViewModel : ViewModelBase<SettingsViewModel>
{
    private int motor1Speed;
    private int motor2Speed;

    public int Motor1Speed { get => motor1Speed; set => Set(ref motor1Speed, value); }
    public int Motor2Speed { get => motor2Speed; set => Set(ref motor2Speed, value); }
}