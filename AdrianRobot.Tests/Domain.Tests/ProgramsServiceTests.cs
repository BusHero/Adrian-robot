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

    [Fact]
    public void UpdateName()
    {
        var program = new Program(new(), "first", 0, Array.Empty<Point>());
        var repository = new InMemoryProgramsRepository(program);
        var programsService = new ProgramsService(repository);

        programsService.UpdateProgramName(program.Id, "New Program Name");

        repository
            .GetProgram(program.Id)
            .Select(program => program.Name)
            .Should()
            .Be("New Program Name".ToOption());
    }
}

public class InMemoryProgramsRepository : IProgramsRepository
{
    #region Constructors

    public InMemoryProgramsRepository(IEnumerable<Program> programs) => Programs = programs.ToDictionary(program => program.Id);

    public InMemoryProgramsRepository(params Program[] programs) : this(programs.AsEnumerable()) { }

    public InMemoryProgramsRepository(IEnumerable<string> programNames) : this(programNames
            .Select(programName => new Program(new (), programName, 0, Array.Empty<Point>()))) { }

    public InMemoryProgramsRepository(params string[] programNames) : this(programNames.AsEnumerable()) { }

    public InMemoryProgramsRepository() : this(Array.Empty<Program>()) { }
    
    #endregion


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