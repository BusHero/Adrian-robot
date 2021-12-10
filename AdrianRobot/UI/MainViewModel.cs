using AdrianRobot.Domain;

using System.Collections.ObjectModel;
using System.Linq;

namespace AdrianRobot;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(IProgramsService programsService)
    {
        ProgramsService = programsService;
        Programs = programsService.GetAllPrograms() switch
        {
            { Count: > 0 } programs => new ObservableCollection<ProgramViewModel>(
                programs.Select(
                    program => new ProgramViewModel(program))),
            _ => new ObservableCollection<ProgramViewModel>()
        };

        if (Programs.Count > 0)
            Programs[0].IsSelected = true;
    }

    public ObservableCollection<ProgramViewModel> Programs { get; }
    public IProgramsService ProgramsService { get; }
}

public class ProgramViewModel : ViewModelBase
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