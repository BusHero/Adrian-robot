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
    }

    public ObservableCollection<ProgramViewModel> Programs { get; }
    public IProgramsService ProgramsService { get; }
}

public class ProgramViewModel
{
    public ProgramViewModel(Program program)
    {
        Program = program;
    }

    public Program Program { get; }

    public string Name => Program.Name;
}