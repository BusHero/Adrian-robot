namespace AdrianRobot.Domain;

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
