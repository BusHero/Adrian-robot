using System;
using System.Collections.Immutable;
using System.Linq;

namespace AdrianRobot.Domain;

public class ProgramsService : IProgramsService
{
    public ProgramsService(IProgramsRepository programsRepository)
    {
        ProgramsRepository = programsRepository ?? throw new ArgumentNullException(nameof(programsRepository));
    }

    private IProgramsRepository ProgramsRepository { get; }

    public Program CreateProgram(string productName)
    {
        var program = new Program(new ProgramId(), productName, 0);
        ProgramsRepository.SaveProgram(program);
        return program;
    }

    public Option<string> GetProgramName(ProgramId programId) => from program in ProgramsRepository.GetProgram(programId)
                                                                 select program.Name;

    public ImmutableList<string> GetAllProgramNames() => ProgramsRepository
        .GetAllPrograms()
        .Select(program => program.Name)
        .ToImmutableList();

    public ImmutableList<Program> GetAllPrograms() => ProgramsRepository
        .GetAllPrograms();
}
