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
    public void RemoveProgram()
    {
        var program = new Program(new(), "first", 0, Array.Empty<Point>());
        var repository = new InMemoryProgramsRepository(program);
        IProgramsService programsService = new ProgramsService(repository);

        programsService.RemoveProgram(program.Id);

        repository.GetAllPrograms().Should().BeEmpty();
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
