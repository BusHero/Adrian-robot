using AdrianRobot.Domain;

namespace AdrianRobot.Tests;

public class ProgramExecutionServiceTests
{
    [Fact]
    public async Task Foo()
    {
        var program = new Program(new(), "Program", 30, Arrays.Of<Point>(
            new (new(), "Point 1", 100, 100),
            new (new(), "Point 1", 100, 100),
            new (new(), "Point 1", 100, 100),
            new (new(), "Point 1", 100, 100)));

        var programExecutionService = new ProgramsExecutionService();

        await programExecutionService.ExecuteProgramAsync(program.Id);
    }
}
