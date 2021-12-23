using AdrianRobot.Domain;

namespace AdrianRobot
{
    public interface IProgramsExecutionService
    {
        event EventHandler<CommandExecutedEventArgs>? CommandExecutedEvent;

        Task ExecuteProgramAsync(ProgramId id);
    }
}