using AdrianRobot.Domain;

namespace AdrianRobot;

public class ProgramViewModel : ViewModelBase<ProgramViewModel>
{
    private bool isSelected;

    public ProgramViewModel(Program program, bool isSelected = false)
    {
        Program = program;
        IsSelected = isSelected;
    }

    public Program Program { get; }

    public string Name => Program.Name;

    public bool IsSelected { get => isSelected; set => Set(ref isSelected, value); }
}