using AdrianRobot.Domain;

namespace AdrianRobot
{
    public interface IProgramsExecutionService
    {
        IProgramsRepository ProgramsRepository { get; }

        event EventHandler<CommandExecutedEventArgs>? CommandExecutedEvent;

        Task ExecuteProgramAsync(ProgramId id);
    }
}