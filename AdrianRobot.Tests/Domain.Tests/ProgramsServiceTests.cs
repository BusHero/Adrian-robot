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
    public void GetNames()
    {
        var programs = new[] { "first", "second" };
        var repository = new InMemoryProgramsRepository(programs);
        var programsService = new ProgramsService(repository, Substitute.For<IPointsService>());

        ImmutableList<string> actualPrograms = programsService.GetAllProgramNames();
        actualPrograms.Should().BeEquivalentTo(programs);
    }
    
    [Fact]
    public void CanCreateAProgram()
    {
        var repository = new InMemoryProgramsRepository();
        const string programName = "Test program";
        var programsService = new ProgramsService(repository, Substitute.For<IPointsService>());
        
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
        IProgramsService programsService = new ProgramsService(repository, Substitute.For<IPointsService>());

        programsService.RemoveProgram(program.Id);

        repository.GetAllPrograms().Should().BeEmpty();
    }

    [Fact]
    public void UpdateName()
    {
        var program = new Program(new(), "first", 0, Array.Empty<Point>());
        var repository = new InMemoryProgramsRepository(program);
        var programsService = new ProgramsService(repository, Substitute.For<IPointsService>());

        programsService.UpdateProgramName(program.Id, "New Program Name");

        repository
            .GetProgram(program.Id)
            .Select(program => program.Name)
            .Should()
            .Be("New Program Name".ToOption());
    }

    [Fact]
    public void AddPoint()
    {
        var pointsService = new PointsService(new InMemoryPointRepository());
        var point = pointsService.CreatePoint();
        var program = new Program(new(), "first", 0, Array.Empty<Point>());
        var programsRepository = new InMemoryProgramsRepository(program);
        var programsService = new ProgramsService(programsRepository, pointsService);

        programsService.AddPoint(program.Id, point.Id, wait: 0, shake: 0);

        programsRepository.GetProgram(program.Id)
            .Select(program => program.Points[0].PointId)
            .Should()
            .Be(point.Id.ToOption());
    }

    [Fact]
    public void RemovePoint()
    {
        var point = new Point(new(), "point", 10, 10);
        var pointsService = new PointsService(new InMemoryPointRepository(point));
        var program = new Program(new(), "first", 0, new[] { point });
        var programPointId = program.Points[0].Id;
        var programsRepository = new InMemoryProgramsRepository(program);
        var programsService = new ProgramsService(programsRepository, pointsService);

        programsService.RemovePoint(program.Id, programPointId);

        programsRepository.GetProgram(program.Id)
            .Select(program => program.Points.Count)
            .Should()
            .Be(0.ToOption());
    }

    [Fact]
    public void UpdatePointWait()
    {
        var point = new Point(new(), "point", 10, 10);
        var pointsService = new PointsService(new InMemoryPointRepository(point));
        var program = new Program(new(), "first", 0, new[] { point });
        var programPointId = program.Points[0].Id;
        var programsRepository = new InMemoryProgramsRepository(program);
        var programsService = new ProgramsService(programsRepository, pointsService);

        programsService.UpdatePointWait(program.Id, programPointId, 30);

        programsRepository.GetProgram(program.Id)
            .Select(program => program.Points[0].Wait)
            .Should()
            .Be(30.ToOption());
    }

    [Fact]
    public void UpdatePointShake()
    {
        var point = new Point(new(), "point", 10, 10);
        var pointsService = new PointsService(new InMemoryPointRepository(point));
        var program = new Program(new(), "first", 0, new[] { point });
        var programPointId = program.Points[0].Id;
        var programsRepository = new InMemoryProgramsRepository(program);
        var programsService = new ProgramsService(programsRepository, pointsService);

        programsService.UpdatePointShake(program.Id, programPointId, shake: 30);

        programsRepository.GetProgram(program.Id)
            .Select(program => program.Points[0].Shake)
            .Should()
            .Be(30.ToOption());
    }
}
