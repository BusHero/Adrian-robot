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
        Wait = Point.Wait;
        Shake = Point.Shake;
    }

    public ProgramPoint Point { get; }

    public string Name => $"{Point.Name} (y: {Point.MotorYPosition}, z: {Point.MotorZPosition})";

    public int Wait { get; set; }

    public int Shake { get; set; }

    public ICommand RemoveCommand { get; }

    public bool IsRemoved { get => isRemoved; set => Set(ref isRemoved, value); }
}
