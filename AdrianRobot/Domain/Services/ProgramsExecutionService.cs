using AdrianRobot.Domain;

namespace AdrianRobot;

public class ProgramsExecutionService
{
    public IProgramsRepository ProgramsRepository { get; }

    public ProgramsExecutionService(IProgramsRepository programsRepository)
    {
        ProgramsRepository = programsRepository ?? throw new ArgumentNullException(nameof(programsRepository));
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

            foreach (var point in program.Points)
            {
                FireCommandExecutedEvent($"Navigating to {point.Name}(y: {point.MotorYPosition}, z: {point.MotorZPosition})");
                FireCommandExecutedEvent($"Waiting for {point.Wait} seconds");
                if (point.Shake != 0)
                {
                    FireCommandExecutedEvent($"Shaking for {point.Shake} seconds");
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
