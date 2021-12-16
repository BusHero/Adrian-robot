using AdrianRobot.Domain;

using System.Collections.ObjectModel;

namespace AdrianRobot;

public class ProgramOverviewViewModel : ViewModelBase<ProgramOverviewViewModel>
{
    #region Private Fields

    private string name = default!;
    private int repeats;

    #endregion
    
    #region Private Services

    private IProgramsService ProgramsService { get; }
    private IPointsService PointsService { get; }

    #endregion

    #region Properties

    public Program Program { get; }

    public ObservableCollection<PointViewModel> Points { get; }

    public string Name { get => name; set => Set(ref name, value); }

    public int Repeats { get => repeats; set => Set(ref repeats, value); }
    public ObservableCollection<PossiblePointViewModel> PossiblePoints { get; }

    #endregion

    public ProgramOverviewViewModel(Program program,
        IProgramsService programsService, 
        IPointsService pointsService)
    {
        ProgramsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        PointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
        Program = program ?? throw new ArgumentNullException(nameof(program));

        Points = Program.Points.Select(ToPointViewModel).ToObservableCollection();
        Name = Program.Name;
        Repeats = Program.Repeats;
        PossiblePoints = PointsService.GetPoints().Select(point => new PossiblePointViewModel(point)).ToObservableCollection();
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

    private PointViewModel ToPointViewModel(ProgramPoint point)
    {
        var pointViewModel = new PointViewModel(point);
        pointViewModel.SubscribePropertyChanged(nameof(pointViewModel.IsRemoved), @this => HandleRemovePoint(@this.Point));
        return pointViewModel;
    }

    private void HandleRemovePoint(ProgramPoint point)
    {
        ProgramsService.RemovePoint(Program.Id, point.Id);
        Program.RemovePoint(point.Id);
        Points.Remove(Points.First(pointViewModel => pointViewModel.Point == point));
    }

    #endregion
}
