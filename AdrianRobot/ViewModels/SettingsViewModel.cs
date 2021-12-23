using System.Collections.ObjectModel;
using System.Windows.Input;

using AdrianRobot.Domain;

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
        AddPointCommand = Commands.NewCommand(HandleAddPointCommand);

        Points = PointsService
            .GetPoints()
            .Select(CreateSettingsPointViewModel)
            .ToObservableCollection();

        Motor1Speed = SettingsRepository.Motor1Speed;
        Motor2Speed = SettingsRepository.Motor2Speed;
    }

    public SettingsPointViewModel CreateSettingsPointViewModel(Point point)
    {
        var pointViewModel = new SettingsPointViewModel(point, PointsService);
        pointViewModel.SubscribePropertyChanged(nameof(pointViewModel.IsRemoved), HandleRemovePoint);
        return pointViewModel;
    }

    private void HandleAddPointCommand()
    {
        var point = PointsService.CreatePoint();
        var pointViewModel = CreateSettingsPointViewModel(point);
        Points.Add(pointViewModel);
    }

    private void HandleRemovePoint(SettingsPointViewModel point)
    {
        _ = Points.Remove(point);
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
