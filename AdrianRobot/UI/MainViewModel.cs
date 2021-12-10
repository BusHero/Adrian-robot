namespace AdrianRobot;

public class MainViewModel : ViewModelBase
{
    #region Private fields

    private ViewModelBase current;

    #endregion

    public MainViewModel(SettingsViewModel settingsViewModel, ProgramsViewModel programsViewModel)
    {
        SettingsViewModel = settingsViewModel ?? throw new System.ArgumentNullException(nameof(settingsViewModel));
        ProgramsViewModel = programsViewModel ?? throw new System.ArgumentNullException(nameof(programsViewModel));
        CurrentViewModel = SettingsViewModel;
    }

    public ViewModelBase CurrentViewModel { get => current; set => Set(ref current, value); }
    public SettingsViewModel SettingsViewModel { get; }
    public ProgramsViewModel ProgramsViewModel { get; }
}

public class SettingsViewModel: ViewModelBase
{

}

public class ProgramsViewModel: ViewModelBase
{

}