using System.Windows.Input;

namespace AdrianRobot;

public class PossiblePointViewModel: ViewModelBase<PossiblePointViewModel>
{
    public bool isSelected;

    public PossiblePointViewModel(Point point)
    {
        Point = point ?? throw new ArgumentNullException(nameof(point));
        IsSelected = false;
        SelectPointCommand = Commands.NewCommand(() => IsSelected = true);
    }

    public string Name => $"{Point.Name}(y: {Point.MotorYPosition}, z: {Point.MotorZPosition})";

    public Point Point { get; }

    public bool IsSelected { get => isSelected; set => Set(ref isSelected, value); }

    public ICommand SelectPointCommand { get; set; }
}
