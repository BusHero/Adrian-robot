using AdrianRobot.Domain;

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdrianRobot;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private ViewModelBase? selected;
    private ProgramViewModel? selectedProgram;
    private bool isSettingsSelected;

    private SettingsViewModel settingsViewModel;

    #endregion

    #region Properties

    public ObservableCollection<ProgramViewModel> Programs { get; }

    public ViewModelBase? Selected { get => selected; set => Set(ref selected, value); }

    public ProgramViewModel? SelectedProgram { get => selectedProgram; set => Set(ref selectedProgram, value); }

    public ICommand AddProgramCommand { get; }

    public bool IsSettingsSelected { get => isSettingsSelected; set => Set(ref isSettingsSelected, value); }

    #endregion

    #region Services

    private IProgramsService ProgramsService { get; }

    #endregion

    public MainViewModel(IProgramsService programsService)
    {
        ProgramsService = programsService;

        AddProgramCommand = Commands.NewCommand(CreateNewProgram);

        settingsViewModel = new SettingsViewModel();
        Programs = programsService.GetAllPrograms() switch
        {
            { Count: > 0 } programs => new ObservableCollection<ProgramViewModel>(
                programs.Select(CreateProgramViewModel)),
            _ => new ObservableCollection<ProgramViewModel>()
        };

        if (Programs.Count == 0)
            return;

        Programs[0].IsSelected = true;

        SubscribePropertyChanged(nameof(SelectedProgram), SelectedProgramChanged);
        SubscribePropertyChanged(nameof(IsSettingsSelected), SelectSettingsView);
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

    private void UpdateSelectedProperties(ProgramViewModel? program)
    {
        if (program is null || program.IsSelected == false)
            return;

        UnselectAllProgramsExcept(program);
        Selected = new ProgramOverviewViewModel(program.Program);
    }

    private void SelectedProgramChanged()
    {
        if (SelectedProgram == null)
            UnselectAllPrograms();
        else
            SelectedProgram.IsSelected = true;
    }

    private void SelectSettingsView()
    {
        if (IsSettingsSelected != true)
            return;

        UnselectAllPrograms();
        Selected = settingsViewModel;
    }

    private void UnselectAllPrograms()
    {
        foreach (var program in Programs)
            program.IsSelected = false;
    }

    private void UnselectAllProgramsExcept(ProgramViewModel except)
    {
        foreach (var program in Programs.Where(program => program != except))
            program.IsSelected = false;
    }
    #endregion
}
