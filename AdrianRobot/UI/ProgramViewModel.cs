using AdrianRobot.Domain;

using System.Windows.Input;

namespace AdrianRobot;

public class ProgramViewModel : ViewModelBase<ProgramViewModel>
{
    private bool isSelected;
    private bool isDeleted;

    public ProgramViewModel(Program program, bool isSelected = false)
    {
        Program = program;
        IsSelected = isSelected;
        IsDeleted = false;
        DeleteProgramCommand = Commands.NewCommand(() => IsDeleted = true);
        SelectCommand = Commands.NewCommand(() => IsSelected = true);
    }

    public Program Program { get; }

    public string Name => Program.Name;

    public bool IsSelected { get => isSelected; set => Set(ref isSelected, value); }

    public bool IsDeleted { get => isDeleted; set => Set(ref isDeleted, value); }

    public ICommand DeleteProgramCommand { get; }

    public ICommand SelectCommand { get; }
}