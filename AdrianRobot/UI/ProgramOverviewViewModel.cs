using AdrianRobot.Domain;

using System;

namespace AdrianRobot;

public class ProgramOverviewViewModel : ViewModelBase<ProgramOverviewViewModel>
{
    public ProgramOverviewViewModel(Program program)
    {
        Program = program ?? throw new ArgumentNullException(nameof(program));
    }

    public Program Program { get; }

    public string Name => Program.Name;

    public int Repeats => Program.Repeats;

    public void UpdateRepeats(int v)
    {
        throw new NotImplementedException();
    }
}
