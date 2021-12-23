using AdrianRobot.Domain;

using FluentAssertions.Events;

namespace AdrianRobot.Tests;

public class ProgramExecutionServiceTests
{
    [Fact]
    public async Task Foo()
    {
        var points = Arrays.Of<Point>(
            new(new(), "Point 1", 100, 200),
            new(new(), "Point 2", 10, 20));
        var pointsRepository = new InMemoryPointsRepository();
        pointsRepository.SavePoint(points[0]);
        pointsRepository.SavePoint(points[1]);

        var program = new Program(new(), "Program", 3, points);
        program.UpdatePointWait(program.Points[0].Id, 10);
        program.UpdatePointShake(program.Points[0].Id, 0);

        program.UpdatePointWait(program.Points[1].Id, 5);
        program.UpdatePointShake(program.Points[1].Id, 15);


        var repository = Substitute.For<IProgramsRepository>();
        
        repository.GetProgram(program.Id).Returns(program.ToOption());
        
        var programExecutionService = new ProgramsExecutionService(repository, pointsRepository);
        var monitor = programExecutionService.Monitor();

        await programExecutionService.ExecuteProgramAsync(program.Id);

        var events = monitor.OccurredEvents;
        events.Select(OccurredEvent => OccurredEvent.Parameters[1])
            .Should().BeEquivalentTo(new CommandExecutedEventArgs[]
            {
                new ($"Start Executing {program.Name}"),

                new ($"Start cycle 1 of 3"),
                new ($"Navigating to Point 1(y: 100, z: 200)"),
                new ($"Waiting for 10 seconds"),
                new ($"Navigating to Point 2(y: 10, z: 20)"),
                new ($"Waiting for 5 seconds"),
                new ($"Shaking for 15 seconds"),
                new ($"Finish cycle 1 of 3"),

                new ($"Start cycle 2 of 3"),
                new ($"Navigating to Point 1(y: 100, z: 200)"),
                new ($"Waiting for 10 seconds"),
                new ($"Navigating to Point 2(y: 10, z: 20)"),
                new ($"Waiting for 5 seconds"),
                new ($"Shaking for 15 seconds"),
                new ($"Finish cycle 2 of 3"),

                new ($"Start cycle 3 of 3"),
                new ($"Navigating to Point 1(y: 100, z: 200)"),
                new ($"Waiting for 10 seconds"),
                new ($"Navigating to Point 2(y: 10, z: 20)"),
                new ($"Waiting for 5 seconds"),
                new ($"Shaking for 15 seconds"),
                new ($"Finish cycle 3 of 3"),

                new ($"Finish Executing {program.Name}"),
            });
    }
}
