using System.Collections.Immutable;

namespace AdrianRobot.Domain
{
    public interface IProgramsService
    {
        ProgramId CreateProgram(string productName);
        ImmutableList<string> GetAllProgramNames();

        ImmutableList<Program> GetAllPrograms();

        Option<string> GetProgramName(ProgramId programId);
    }
}