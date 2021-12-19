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
