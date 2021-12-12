
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
            new[] { new Point(new PointId(), "Point", 100, 100) });
        var sut = new ProgramOverviewViewModel(program);
        
        sut.Points.Count.Should().Be(1);
    }
}

public class TestProgramService : IProgramsService
{
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
}