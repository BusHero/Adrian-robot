using AdrianRobot.Domain;

namespace AdrianRobot.Domain;

public record ProgramNameUpdatedEventArgs(ProgramId ProgramId, string Name);

public record ProgramRepeatsUpdateEventArgs(ProgramId ProgramId, int Repeats);
