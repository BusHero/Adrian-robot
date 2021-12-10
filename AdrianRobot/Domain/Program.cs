namespace AdrianRobot.Domain;

public class Program
{
    public Program(ProgramId id, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new System.ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        Id = id ?? throw new System.ArgumentNullException(nameof(id));
        Name = name;
    }

    public ProgramId Id { get; }
    public string Name { get; }
}
