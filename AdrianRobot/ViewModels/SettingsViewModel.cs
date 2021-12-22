using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdrianRobot;

public class SettingsViewModel : ViewModelBase<SettingsViewModel>
{
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
            .Select(point => new SettingsPointViewModel(point, PointsService))
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

public class SettingsPointViewModel : ViewModelBase<PointViewModel>
{
    private readonly IPointsService pointsService;
    
    public SettingsPointViewModel(Point point, IPointsService pointsService)
    {
        Point = point ?? throw new ArgumentNullException(nameof(point));
        this.pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
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
}