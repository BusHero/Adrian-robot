using AdrianRobot.Domain;

using System;
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
                programs.Select(CreateProgramViewModel)),
            _ => new ObservableCollection<ProgramViewModel>()
        };

        if (Programs.Count > 0)
            Programs[0].IsSelected = true;
    }

    private ProgramViewModel CreateProgramViewModel(Program program)
    {
        var programViewModel = new ProgramViewModel(program);

        programViewModel.SubscribePropertyChanged(nameof(programViewModel.IsSelected), UpdateSelectedProperties);

        return programViewModel;
    }

    private void UpdateSelectedProperties(ProgramViewModel program)
    {
        if (program.IsSelected == false)
            return;

        foreach (var bar in Programs.Where(foo => foo != program))
            bar.IsSelected = false;
    }

    public ObservableCollection<ProgramViewModel> Programs { get; }
    public IProgramsService ProgramsService { get; }
}
