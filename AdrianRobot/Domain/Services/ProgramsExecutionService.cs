using AdrianRobot.Domain;

namespace AdrianRobot;

public class ProgramsExecutionService : IProgramsExecutionService
{
    private IProgramsRepository ProgramsRepository { get; }
    private IPointsRepository PointsRepository { get; }

    public ProgramsExecutionService(IProgramsRepository programsRepository, IPointsRepository pointsRepository)
    {
        ProgramsRepository = programsRepository ?? throw new ArgumentNullException(nameof(programsRepository));
        PointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
    }

    public async Task ExecuteProgramAsync(ProgramId id)
    {
        await Task.Run(() => ExecuteProgram(id));
    }

    private void ExecuteProgram(ProgramId id)
    {
        var program = ProgramsRepository.GetProgram(id);
        program.Modify(ExecuteProgram);
    }

    public void ExecuteProgram(Program program)
    {
        FireCommandExecutedEvent($"Start Executing {program.Name}");

        for (var cycle = 1; cycle < program.Repeats + 1; cycle++)
        {
            FireCommandExecutedEvent($"Start cycle {cycle} of {program.Repeats}");

            foreach (var programPoint in program.Points)
            {
                var point = PointsRepository.GetPoint(programPoint.PointId).ValueOrDefault(Point.Empty);

                FireCommandExecutedEvent($"Navigating to {point.Name}(y: {point.MotorYPosition}, z: {point.MotorZPosition})");
                FireCommandExecutedEvent($"Waiting for {programPoint.Wait} seconds");
                if (programPoint.Shake != 0)
                {
                    FireCommandExecutedEvent($"Shaking for {programPoint.Shake} seconds");
                }
            }

            FireCommandExecutedEvent($"Finish cycle {cycle} of {program.Repeats}");
        }


        FireCommandExecutedEvent($"Finish Executing {program.Name}");
    }



    private void FireCommandExecutedEvent(string command) => CommandExecutedEvent?.Invoke(this, new(command));

    public event EventHandler<CommandExecutedEventArgs>? CommandExecutedEvent;
}

public class CommandExecutedEventArgs: EventArgs
{
    public CommandExecutedEventArgs(string command)
    {
        Command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public string Command { get; }
}
