using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdrianRobot;

public class SettingsViewModel : ViewModelBase<SettingsViewModel>
{
    #region Private Fields

    private int motor1Speed;
    
    private int motor2Speed;

    #endregion

    #region Services

    private IPointsService PointsService { get; }
    
    #endregion

    public SettingsViewModel(IPointsService pointsService)
    {
        PointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));

        AddPointCommand = Commands.NewCommand(() => PointsService.CreatePoint());

        Points = PointsService
            .GetPoints()
            .Select(point => new SettingsPointViewModel(point))
            .ToObservableCollection();
    }

    #region Public Fields

    public int Motor1Speed { get => motor1Speed; set => Set(ref motor1Speed, value); }
    
    public int Motor2Speed { get => motor2Speed; set => Set(ref motor2Speed, value); }
    
    public ObservableCollection<SettingsPointViewModel> Points { get; set; }
    public ICommand AddPointCommand { get; }

    #endregion
}

public class SettingsPointViewModel
{
    public SettingsPointViewModel(Point point)
    {
        Point = point ?? throw new ArgumentNullException(nameof(point));
    }

    public Point Point { get; }
}