namespace AdrianRobot.Domain;

public class ProgramNameUpdatedEventArgs: EventArgs
{
    public ProgramNameUpdatedEventArgs(ProgramId programId, string Name)
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new ArgumentException($"'{nameof(Name)}' cannot be null or empty.", nameof(Name));
        }

        ProgramId = programId ?? throw new ArgumentNullException(nameof(programId));
        this.Name = Name;
    }

    public ProgramId ProgramId { get; }
    public string Name { get; }
}

public class ProgramRepeatsUpdateEventArgs : EventArgs
{
    public ProgramRepeatsUpdateEventArgs(ProgramId ProgramId, int Repeats)
    {
        this.ProgramId = ProgramId ?? throw new ArgumentNullException(nameof(ProgramId));
        this.Repeats = Repeats;
    }

    public ProgramId ProgramId { get; }
    public int Repeats { get; }
}
