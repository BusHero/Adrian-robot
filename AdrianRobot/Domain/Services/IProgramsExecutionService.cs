using AdrianRobot.Domain;

namespace AdrianRobot
{
    public interface IProgramsExecutionService
    {
        IProgramsRepository ProgramsRepository { get; }

        event EventHandler<CommandExecutedEventArgs>? CommandExecutedEvent;

        void ExecuteProgram(Program program);
        Task ExecuteProgramAsync(ProgramId id);
    }
}