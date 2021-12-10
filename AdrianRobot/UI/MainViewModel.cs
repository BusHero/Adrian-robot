using AdrianRobot.Domain;

using System.Collections.ObjectModel;
using System.Linq;

namespace AdrianRobot;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private ViewModelBase? selected;

    #endregion

    #region Properties

    public ObservableCollection<ProgramViewModel> Programs { get; }

    public ViewModelBase? Selected { get => selected; set => Set(ref selected, value); }

    #endregion

    #region Services

    private IProgramsService ProgramsService { get; }

    #endregion

    public MainViewModel(IProgramsService programsService)
    {
        ProgramsService = programsService;
        Programs = programsService.GetAllPrograms() switch
        {
            { Count: > 0 } programs => new ObservableCollection<ProgramViewModel>(
                programs.Select(CreateProgramViewModel)),
            _ => new ObservableCollection<ProgramViewModel>()
        };

        if (Programs.Count == 0)
            return;
        
        Programs[0].IsSelected = true;
    }

    #region Public Methdods

    public void CreateNewProgram()
    {
        var program = ProgramsService.CreateProgram("New Program");
        var programViewModel = CreateProgramViewModel(program);
        Programs.Add(programViewModel);
    }

    #endregion

    #region Private Methods

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

        Selected = new ProgramOverviewViewModel(program.Program);
    }

    #endregion

}
