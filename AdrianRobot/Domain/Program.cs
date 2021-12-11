namespace AdrianRobot.Domain;

public class Program
{
    public Program(ProgramId id, string name, int repeats)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new System.ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        Id = id ?? throw new System.ArgumentNullException(nameof(id));
        Name = name;
        Repeats = repeats;
    }

    public ProgramId Id { get; }
    public string Name { get; }
    public int Repeats { get; }
}
