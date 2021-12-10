using System.Collections.Immutable;

namespace AdrianRobot.Domain
{
    public interface IProgramsService
    {
        Program CreateProgram(string productName);
        ImmutableList<string> GetAllProgramNames();

        ImmutableList<Program> GetAllPrograms();

        Option<string> GetProgramName(ProgramId programId);
    }
}