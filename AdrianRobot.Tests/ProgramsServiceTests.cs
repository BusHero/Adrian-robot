using AdrianRobot.Domain;

using FluentAssertions;

using NSubstitute;

using System.Collections.Generic;

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
        
        ProgramId id = programsService.CreateProgram(productName: programName);
        id.Should().NotBeNull();

        var name = programsService.GetProgramName(programId: id);
        name.Should().Be(programName.ToOption());
    }
}

public class InMemoryProgramsRepository : IProgramsRepository
{
    private Dictionary<ProgramId, Program> Programs { get; } = new Dictionary<ProgramId, Program>();

    public Option<Program> GetProgram(ProgramId programId) => Programs.TryGetValue(programId, out var program) switch
    {
        true => program.ToOption(),
        false => Option.None<Program>()
    };

    public void SaveProgram(Program program)
    {
        Programs[program.Id] = program;
    }
}