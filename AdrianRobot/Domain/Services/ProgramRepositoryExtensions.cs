namespace AdrianRobot.Domain;

public static class ProgramRepositoryExtensions
{
    public static void SaveProgram(this IProgramsRepository programsRepository, Option<Program> optionalProgram)
    {
        ArgumentNullException.ThrowIfNull(programsRepository);
        ArgumentNullException.ThrowIfNull(optionalProgram);

        if (optionalProgram is Some<Program> { Value: var program})
            programsRepository.SaveProgram(program);
    }
}
