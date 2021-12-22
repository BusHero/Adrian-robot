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
    public ISettingsRepository SettingsRepository { get; }

    #endregion

    public SettingsViewModel(IPointsService pointsService, ISettingsRepository settingsRepository)
    {
        PointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
        SettingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
        AddPointCommand = Commands.NewCommand(() => PointsService.CreatePoint());

        Points = PointsService
            .GetPoints()
            .Select(point => new SettingsPointViewModel(point))
            .ToObservableCollection();

        Motor1Speed = SettingsRepository.Motor1Speed;
        Motor2Speed = SettingsRepository.Motor2Speed;
    }

    #region Public Fields

    public int Motor1Speed
    {
        get => SettingsRepository.Motor1Speed; set
        {
            SettingsRepository.Motor1Speed = value;
            FirePropertyChangedEvent();
        }
    }

    public int Motor2Speed
    {
        get => SettingsRepository.Motor2Speed; set
        {
            SettingsRepository.Motor2Speed = value;
            FirePropertyChangedEvent();
        }
    }

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