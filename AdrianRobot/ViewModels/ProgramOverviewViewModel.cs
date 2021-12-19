using AdrianRobot.Domain;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdrianRobot;

public class ProgramOverviewViewModel : ViewModelBase<ProgramOverviewViewModel>
{
    #region Private Fields

    private string name = default!;
    private int repeats;
    private bool arePossiblePointsShown;

    #endregion

    #region Private Services

    private IProgramsService ProgramsService { get; }
    private IPointsService PointsService { get; }
    private IProgramsExecutionService ProgramsExecutionService { get; }

    #endregion

    #region Properties

    public Program Program { get; }

    public ObservableCollection<PointViewModel> Points { get; }

    public string Name { get => name; set => Set(ref name, value); }

    public int Repeats { get => repeats; set => Set(ref repeats, value); }
    public ObservableCollection<PossiblePointViewModel> PossiblePoints { get; }

    public bool ArePossiblePointsShown { get => arePossiblePointsShown; set => Set(ref arePossiblePointsShown, value); }

    public ICommand ShowPossiblePointsCommand { get; }
    public ICommand ExecuteCommand { get; }

    #endregion

    public ProgramOverviewViewModel(Program program,
        IProgramsService programsService,
        IPointsService pointsService, 
        IProgramsExecutionService programsExecutionService)
    {
        ProgramsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        PointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
        ProgramsExecutionService = programsExecutionService ?? throw new ArgumentNullException(nameof(programsExecutionService));
        Program = program ?? throw new ArgumentNullException(nameof(program));

        Points = Program.Points.Select(ToPointViewModel).ToObservableCollection();
        Name = Program.Name;
        Repeats = Program.Repeats;
        PossiblePoints = PointsService.GetPoints().Select(ToPossiblePointViewModel).ToObservableCollection();
        ShowPossiblePointsCommand = Commands.NewCommand(() => ArePossiblePointsShown = true);
        ExecuteCommand = Commands.NewCommand(HandleExecuteCommand);

        SubscribePropertyChanged(nameof(Repeats), @this => UpdateRepeats(@this.Repeats));
        SubscribePropertyChanged(nameof(Name), @this => UpdateName(@this.Name));
    }

    #region Public Methods

    public void UpdateRepeats(int repeats)
    {
        ProgramsService.UpdateProgramRepeats(Program.Id, repeats);
        Repeats = repeats;
        Program.Repeats = repeats;
    }

    public void UpdateName(string newProgramName)
    {
        ProgramsService.UpdateProgramName(Program.Id, newProgramName);
        Name = newProgramName;
        Program.Name = newProgramName;
    }

    #endregion

    #region Private Methods

    private async void HandleExecuteCommand(object? _)
    {
        await ProgramsExecutionService.ExecuteProgramAsync(Program.Id);
    }

    private PossiblePointViewModel ToPossiblePointViewModel(Point point)
    {
        var pointViewModel = new PossiblePointViewModel(point);

        pointViewModel.SubscribePropertyChanged(nameof(pointViewModel.IsSelected), HandlePossiblePointSelected);

        return pointViewModel;
    }

    private void HandlePossiblePointSelected(PossiblePointViewModel pointViewModel)
    {
        foreach (var possiblePoint in PossiblePoints)
            possiblePoint.isSelected = false;
        ArePossiblePointsShown = false;

        ProgramsService.AddPoint(Program.Id, pointViewModel.Point.Id, 0, 0);
        Program.AddPoint(pointViewModel.Point, 0, 0);
        var programPoint = Program.Points.FirstOrDefault(x => x.PointId == pointViewModel.Point.Id);
        var programPointViewModel = ToPointViewModel(programPoint);
        Points.Add(programPointViewModel);
    }   

    private PointViewModel ToPointViewModel(ProgramPoint point)
    {
        var pointViewModel = new PointViewModel(point);
        pointViewModel.SubscribePropertyChanged(nameof(pointViewModel.IsRemoved), @this => HandleRemovePoint(@this.Point));
        return pointViewModel;
    }

    private void HandleRemovePoint(ProgramPoint point)
    {
        ProgramsService.RemovePoint(Program.Id, point.Id);
        Points.Remove(Points.First(pointViewModel => pointViewModel.Point == point));
        Program.RemovePoint(point.Id);
    }

    #endregion
}
