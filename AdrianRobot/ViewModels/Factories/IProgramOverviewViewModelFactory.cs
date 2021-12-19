using AdrianRobot.Domain;

namespace AdrianRobot;

public interface IProgramOverviewViewModelFactory
{
    ProgramOverviewViewModel CreateProgramOverviewViewModel(Program program);
}

public class ProgramOverviewViewModelFactory : IProgramOverviewViewModelFactory
{
    private readonly IProgramsService programsService;
    private readonly IPointsService pointsService;
    private readonly IProgramsExecutionService programsExecutionService;

    public ProgramOverviewViewModelFactory(IProgramsService programsService, IPointsService pointsService, IProgramsExecutionService programsExecutionService)
    {
        this.programsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        this.pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
        this.programsExecutionService = programsExecutionService ?? throw new ArgumentNullException(nameof(programsExecutionService));
    }

    public ProgramOverviewViewModel CreateProgramOverviewViewModel(Program program)
    {
        return new ProgramOverviewViewModel(program, programsService, pointsService, programsExecutionService);
    }
}

