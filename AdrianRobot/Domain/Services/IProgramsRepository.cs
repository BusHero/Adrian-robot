using System.Collections.Immutable;

namespace AdrianRobot.Domain;

public interface IProgramsRepository
{
    public void SaveProgram(Program program);

    public Option<Program> GetProgram(ProgramId programId);
    ImmutableList<Program> GetAllProducts();
}
