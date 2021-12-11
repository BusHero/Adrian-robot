using AdrianRobot.Domain;

using FluentAssertions;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Xunit;

namespace AdrianRobot.Tests;

public class ProgramsServiceTests
{
    [Fact]
    public void CanCreateAProgram()
    {
        var repository = new InMemoryProgramsRepository();
        const string programName = "Test program";
        var programsService = new ProgramsService(repository);
        
        var program = programsService.CreateProgram(productName: programName);
        program.Should().NotBeNull();

        var name = programsService.GetProgramName(programId: program.Id);
        name.Should().Be(programName.ToOption());
    }

    [Fact]
    public void GetNames()
    {
        var programs = new[] { "first", "second" };
        var repository = new InMemoryProgramsRepository(programs);
        var programsService = new ProgramsService(repository);

        ImmutableList<string> actualPrograms = programsService.GetAllProgramNames();
        actualPrograms.Should().BeEquivalentTo(programs);
    }
}

public class InMemoryProgramsRepository : IProgramsRepository
{
    public InMemoryProgramsRepository() => Programs = new Dictionary<ProgramId, Program>();

    public InMemoryProgramsRepository(IEnumerable<string> programNames) => Programs = programNames
        .Select(programName => new Program(new ProgramId(), programName, 0))
        .ToDictionary(program => program.Id);

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