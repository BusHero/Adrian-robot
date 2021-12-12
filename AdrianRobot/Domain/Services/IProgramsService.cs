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

    void UpdateProgramName(ProgramId programId, string newProgramName);
    void RemoveProgram(ProgramId id);
    void UpdatePointWait(ProgramId programId, ProgramPointId programPointId, int wait);
    void RemovePoint(ProgramId programId, ProgramPointId pointId);
    void AddPoint(ProgramId programId, PointId pointId, int wait, int shake);
    void UpdatePointShake(ProgramId programId, ProgramPointId programPointId, int shake);
}
