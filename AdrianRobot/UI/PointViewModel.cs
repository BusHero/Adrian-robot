using AdrianRobot.Domain;

using System.Windows.Input;

namespace AdrianRobot;

public class PointViewModel: ViewModelBase<PointViewModel>
{
    private bool isRemoved;

    public PointViewModel(ProgramPoint point)
    {
        Point = point ?? throw new ArgumentNullException(nameof(point));
        RemoveCommand = Commands.NewCommand(() => IsRemoved = true);
    }

    public ProgramPoint Point { get; }

    public string Name => $"{Point.Name} (y: {Point.MotorYPosition}, z: {Point.MotorZPosition})";

    public int Wait => Point.Wait;

    public int Shake => Point.Shake;

    public ICommand RemoveCommand { get; }

    public bool IsRemoved { get => isRemoved; set => Set(ref isRemoved, value); }
}
