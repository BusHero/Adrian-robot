using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdrianRobot.Domain;

public class InMemoryProgramsRepository : IProgramsRepository
{
    public InMemoryProgramsRepository() => Programs = new Dictionary<ProgramId, Program>();

    public InMemoryProgramsRepository(IEnumerable<string> programNames) => Programs = programNames
        .Select(programName => new Program(new ProgramId(), programName, 0, Array.Empty<Point>()))
        .ToDictionary(program => program.Id);

    public InMemoryProgramsRepository(params string[] programNames) : this(programNames.AsEnumerable()) { }

    private Dictionary<ProgramId, Program> Programs { get; }

    public Option<Program> GetProgram(ProgramId programId) => Programs.TryGetValue(programId, out var program) switch
    {
        true => program.ToOption(),
        false => Option.None<Program>()
    };

    public void SaveProgram(Program program) => Programs[program.Id] = program;

    public ImmutableList<Program> GetAllPrograms() => Programs
        .Values
        .ToImmutableList();
}