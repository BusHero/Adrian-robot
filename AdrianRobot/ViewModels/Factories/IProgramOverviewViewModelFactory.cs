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

    public ProgramOverviewViewModelFactory(IProgramsService programsService, IPointsService pointsService)
    {
        this.programsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        this.pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
    }

    public ProgramOverviewViewModel CreateProgramOverviewViewModel(Program program)
    {
        return new ProgramOverviewViewModel(program, programsService, pointsService);
    }
}
