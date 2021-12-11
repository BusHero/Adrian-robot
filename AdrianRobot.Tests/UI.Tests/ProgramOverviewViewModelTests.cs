
using AdrianRobot.Domain;

namespace AdrianRobot.Tests;

public class ProgramOverviewViewModelTests
{
    [Fact]
    public void ProgramOverviewHasAName()
    {
        var programName = "Program name";
        var program = new Program(new(), programName, 0);
        var sut = new ProgramOverviewViewModel(program);

        sut.Name.Should().Be(programName);
    }

    [Fact]
    public void ProgramOverviewShowsRepeats()
    {
        var programName = "Program name";
        var repeats = 30;
        var program = new Program(new(), programName, repeats);
        var sut = new ProgramOverviewViewModel(program);

        sut.Repeats.Should().Be(repeats);
    }
}
