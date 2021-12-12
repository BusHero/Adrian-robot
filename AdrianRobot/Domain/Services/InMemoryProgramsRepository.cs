using System.Collections.Immutable;

namespace AdrianRobot.Domain;

public class InMemoryProgramsRepository : IProgramsRepository
{
    #region Constructors

    public InMemoryProgramsRepository(IEnumerable<Program> programs) => Programs = programs.ToDictionary(program => program.Id);

    public InMemoryProgramsRepository(params Program[] programs) : this(programs.AsEnumerable()) { }

    public InMemoryProgramsRepository(IEnumerable<string> programNames) : this(programNames
            .Select(programName => new Program(new(), programName, 0, Array.Empty<Point>())))
    { }

    public InMemoryProgramsRepository(params string[] programNames) : this(programNames.AsEnumerable()) { }

    public InMemoryProgramsRepository() : this(Array.Empty<Program>()) { }

    #endregion


    private Dictionary<ProgramId, Program> Programs { get; }

    #region Public Methods

    public Option<Program> GetProgram(ProgramId programId) => Programs.TryGetValue(programId, out var program) switch
    {
        true => program.ToOption(),
        false => Option.None<Program>()
    };

    public void SaveProgram(Program program) => Programs[program.Id] = program;

    public ImmutableList<Program> GetAllPrograms() => Programs
        .Values
        .ToImmutableList();

    public void RemoveProgram(ProgramId id) => Programs.Remove(id);

    #endregion
}