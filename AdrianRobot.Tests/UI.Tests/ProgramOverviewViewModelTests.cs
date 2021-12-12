
using AdrianRobot.Domain;

using System.Collections.Immutable;

namespace AdrianRobot.Tests;

public class ProgramOverviewViewModelTests
{
    [Fact]
    public void ProgramOverviewHasAName()
    {
        var programName = "Program name";
        var program = new Program(new(), programName, 0, Array.Empty<Point>());
        var sut = new ProgramOverviewViewModel(program);

        sut.Name.Should().Be(programName);
    }

    [Fact]
    public void ProgramOverviewShowsRepeats()
    {
        var programName = "Program name";
        var repeats = 30;
        var program = new Program(new(), programName, repeats, Array.Empty<Point>());
        var sut = new ProgramOverviewViewModel(program);

        sut.Repeats.Should().Be(repeats);
    }

    [Fact]
    public void ProgramOverviewShowsPoints()
    {
        var program = new Program(
            new(),
            "Program name",
            30,
            new Point[] 
            { 
                new (new PointId(), "Point 1", 100, 100), 
                new (new PointId(), "Point", 100, 100)
            });
        var sut = new ProgramOverviewViewModel(program);

        sut.Points
            .Select(point => point.Name)
            .Should()
            .BeEquivalentTo(new[] 
            { 
                "Point 1 (y: 100, z: 100)", 
                "Point (y: 100, z: 100)" 
            });
    }
}

public class TestProgramService : IProgramsService
{
    public void AddPoint(ProgramId programId, PointId pointId, int wait, int shake)
    {
        throw new NotImplementedException();
    }

    public Program CreateProgram(string productName)
    {
        throw new NotImplementedException();
    }

    public ImmutableList<string> GetAllProgramNames()
    {
        throw new NotImplementedException();
    }

    public ImmutableList<Program> GetAllPrograms()
    {
        throw new NotImplementedException();
    }

    public Option<Program> GetProgram(ProgramId program)
    {
        throw new NotImplementedException();
    }

    public Option<string> GetProgramName(ProgramId programId)
    {
        throw new NotImplementedException();
    }

    public void RemovePoint(ProgramId programId, ProgramPointId pointId)
    {
        throw new NotImplementedException();
    }

    public void RemoveProgram(ProgramId id)
    {
        throw new NotImplementedException();
    }

    public void UpdatePointWait(ProgramId programId, ProgramPointId programPointId, int wait)
    {
        throw new NotImplementedException();
    }

    public void UpdateProgramName(ProgramId programId, string newProgramName)
    {
        throw new NotImplementedException();
    }
}