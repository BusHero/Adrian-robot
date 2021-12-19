using AdrianRobot.Domain;

namespace AdrianRobot;

public interface IProgramsViewModelFactory
{
    ProgramViewModel CreateProgramViewModel(Program program);
}

public class ProgramViewModelFactory : IProgramsViewModelFactory

{
    private IProgramsService ProgramsService { get; }

    public ProgramViewModelFactory(IProgramsService programsService)
    {
        ProgramsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
    }


    public ProgramViewModel CreateProgramViewModel(Program program) => new ProgramViewModel(ProgramsService, program);
}
