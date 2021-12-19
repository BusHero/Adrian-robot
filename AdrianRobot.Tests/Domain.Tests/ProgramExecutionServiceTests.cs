using AdrianRobot.Domain;

using FluentAssertions.Events;

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
        var repository = Substitute.For<IProgramsRepository>();
        
        repository.GetProgram(program.Id).Returns(program.ToOption());
        
        var programExecutionService = new ProgramsExecutionService(repository);
        var monitor = programExecutionService.Monitor();

        await programExecutionService.ExecuteProgramAsync(program.Id);

        var events = monitor.OccurredEvents;
        events.Select(OccurredEvent => OccurredEvent.Parameters[1])
            .Should().BeEquivalentTo(new CommandExecutedEventArgs[]
            {
                new ($"Start Executing {program.Name}"),
                new ($"Finish Executing {program.Name}"),
            });
    }
}
