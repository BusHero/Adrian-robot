using AdrianRobot.Domain;

using System.Windows.Input;

namespace AdrianRobot;

public class ProgramViewModel : ViewModelBase<ProgramViewModel>
{
    #region Private fields

    private bool isSelected;
    private bool isDeleted;
    private string name = default!;

    #endregion

    #region Services

    private IProgramsService ProgramsService { get; }

    #endregion

    public ProgramViewModel(
        IProgramsService programsService,
        Program program, bool isSelected = false)
    {
        ProgramsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        Program = program;
        IsSelected = isSelected;
        Name = Program.Name;
        IsDeleted = false;
        DeleteProgramCommand = Commands.NewCommand(() => IsDeleted = true);
        SelectCommand = Commands.NewCommand(() => IsSelected = true);

        ProgramsService.ProgramNameUpdatedEvent += HandleProgramNameUpdated;
    }

    private void HandleProgramNameUpdated(object? sender, ProgramNameUpdatedEventArgs e)
    {
        if (e is { ProgramId: var id } && id == Program.Id)
            Name = e.Name;
    }

    #region Public Properties

    public Program Program { get; }

    public string Name { get => name; set => Set(ref name, value); }

    public bool IsSelected { get => isSelected; set => Set(ref isSelected, value); }

    public bool IsDeleted { get => isDeleted; set => Set(ref isDeleted, value); }

    public ICommand DeleteProgramCommand { get; }

    public ICommand SelectCommand { get; }

    #endregion
}