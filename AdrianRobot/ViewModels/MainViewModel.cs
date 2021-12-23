using AdrianRobot.Domain;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdrianRobot;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private ViewModelBase? selected;
    private bool isSettingsSelected;
    private readonly SettingsViewModel settingsViewModel;

    #endregion

    #region Properties
    public ObservableCollection<ProgramViewModel> Programs { get; }

    public ViewModelBase? Selected { get => selected; set => Set(ref selected, value); }


    public ICommand AddProgramCommand { get; }

    public bool IsSettingsSelected
    {
        get => isSettingsSelected; set
        {
            isSettingsSelected = value;
            FirePropertyChangedEvent(nameof(IsSettingsSelected));
        }
    }

    public ViewModelBase ConsoleViewModel { get; }

    #endregion

    #region Services

    private IProgramsService ProgramsService { get; }
    private IProgramOverviewViewModelFactory ProgramOverviewViewModelFactory { get; }
    private IProgramsViewModelFactory ProgramsViewModelFactory { get; }

    #endregion

    public MainViewModel(
        IProgramsService programsService,
        IPointsService pointsService,
        IProgramsExecutionService programsExecutionService,
        IProgramOverviewViewModelFactory programOverviewViewModelFactory,
        IProgramsViewModelFactory programsViewModelFactory, ISettingsRepository settingsRepository)
    {
        ProgramsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        ProgramOverviewViewModelFactory = programOverviewViewModelFactory ?? throw new ArgumentNullException(nameof(programOverviewViewModelFactory));
        AddProgramCommand = Commands.NewCommand(CreateNewProgram);
        ProgramsViewModelFactory = programsViewModelFactory ?? throw new ArgumentNullException(nameof(programOverviewViewModelFactory));

        ConsoleViewModel = new ConsoleViewModel(programsExecutionService);
        settingsViewModel = new SettingsViewModel(pointsService, settingsRepository);
        Programs = programsService.GetAllPrograms() switch
        {
            { Count: > 0 } programs => programs.Select(CreateProgramViewModel).ToObservableCollection(),
            _ => new ObservableCollection<ProgramViewModel>()
        };

        if (Programs.Count != 0)
            Programs[0].IsSelected = true;

        _ = SubscribePropertyChanged(nameof(IsSettingsSelected), SelectSettingsView);
    }

    #region Public Methdods

    public void CreateNewProgram()
    {
        var program = ProgramsService.CreateProgram("New Program");
        
        var programViewModel = CreateProgramViewModel(program);

        Programs.Add(programViewModel);
        UnselectAllPrograms();
        programViewModel.IsSelected = true;
    }

    #endregion

    #region Private Methods

    private void DeleteProgramViewModel(ProgramViewModel programViewModel)
    {
        Programs.Remove(programViewModel);
        ProgramsService.RemoveProgram(programViewModel.Program.Id);
        UnselectAllPrograms();
        if (Programs.Count != 0)
            Programs[0].IsSelected = true;
        else
        {
            Selected = default;
        }
    }

    private ProgramViewModel CreateProgramViewModel(Program program)
    {
        var programViewModel = ProgramsViewModelFactory.CreateProgramViewModel(program);

        programViewModel.SubscribePropertyChanged(nameof(programViewModel.IsSelected), UpdateSelectedProperties);
        programViewModel.SubscribePropertyChanged(nameof(programViewModel.IsDeleted), DeleteProgramViewModel);

        return programViewModel;
    }

    private void UpdateSelectedProperties(ProgramViewModel? program)
    {
        if (program is null || program.IsSelected == false)
            return;

        UnselectAllProgramsExcept(program);
        Selected = ProgramOverviewViewModelFactory.CreateProgramOverviewViewModel(program.Program);
        IsSettingsSelected = false;
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
