using AdrianRobot.Domain;

using System.Collections.Immutable;

namespace AdrianRobot;

public interface IProgramsService
{
    Program CreateProgram(string productName);

    Option<Program> GetProgram(ProgramId program);

    ImmutableList<string> GetAllProgramNames();

    ImmutableList<Program> GetAllPrograms();

    Option<string> GetProgramName(ProgramId programId);
}
