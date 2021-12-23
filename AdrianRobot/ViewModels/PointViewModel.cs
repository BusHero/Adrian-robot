using AdrianRobot.Domain;

using System.Windows.Input;

namespace AdrianRobot;

public class PointViewModel : ViewModelBase<PointViewModel>
{
    private readonly ProgramId programId;
    private readonly IProgramsService programsService;
    private readonly IPointsService pointsService;
    private bool isRemoved;
    private string name;

    public PointViewModel(ProgramId programId, ProgramPoint programPoint, IProgramsService programsService, IPointsService pointsService)
    {
        this.programId = programId ?? throw new ArgumentNullException(nameof(programId));
        Point = programPoint ?? throw new ArgumentNullException(nameof(programPoint));
        this.programsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        this.pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
        RemoveCommand = Commands.NewCommand(HandleRemoveCommand);

        var point = pointsService.GetPoint(programPoint.PointId).ValueOrDefault(AdrianRobot.Point.Empty);
        Name = $"{point.Name} (y: {point.MotorYPosition}, z: {point.MotorZPosition})";
    }

    private void HandleRemoveCommand()
    {
        programsService.RemovePoint(programId, Point.Id);
        IsRemoved = true;
    }

    public ProgramPoint Point { get; private set; }

    public string Name { get => name; set => Set(ref name, value); }

    public int Wait
    {
        get => Point.Wait;
        set
        {
            Point = Point with { Wait = value };
            programsService.UpdatePointWait(programId, Point.Id, value);
            FirePropertyChangedEvent(nameof(Wait));
        }
    }

    public int Shake
    {
        get => Point.Shake;
        set
        {
            Point = Point with { Shake = value };
            programsService.UpdatePointShake(programId, Point.Id, value);
            FirePropertyChangedEvent(nameof(Shake));
        }
    }

    public ICommand RemoveCommand { get; }

    public bool IsRemoved { get => isRemoved; set => Set(ref isRemoved, value); }
}
