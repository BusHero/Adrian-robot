using System.Windows.Input;

namespace AdrianRobot;

public class SettingsPointViewModel : ViewModelBase<SettingsPointViewModel>
{
    private readonly IPointsService pointsService;
    private bool isRemoved;

    public SettingsPointViewModel(Point point, IPointsService pointsService)
    {
        Point = point ?? throw new ArgumentNullException(nameof(point));
        this.pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
        RemoveCommand = Commands.NewCommand(HandleRemoveCommand);
    }

    private void HandleRemoveCommand()
    {
        pointsService.RemovePoint(Point.Id);
        IsRemoved = true;
    }

    public Point Point { get; }

    public string Name
    {
        get => Point.Name;
        set
        {
            Point.Name = value;
            pointsService.UpdatePointName(Point.Id, value);
            FirePropertyChangedEvent(nameof(Name));
        }
    }
    
    public int MotorYPosition
    {
        get => Point.MotorYPosition;
        set
        {
            Point.MotorYPosition = value;
            pointsService.UpdateMotorYPosition(Point.Id, value);
            FirePropertyChangedEvent(nameof(MotorYPosition));
        }
    }

    public int MotorZPosition
    {
        get => Point.MotorZPosition;
        set
        {
            Point.MotorZPosition = value;
            pointsService.UpdateMotorZPosition(Point.Id, value);
            FirePropertyChangedEvent(nameof(MotorZPosition));
        }
    }

    public ICommand RemoveCommand { get; }

    public bool IsRemoved { get => isRemoved; set => Set(ref isRemoved, value); }
}