using System.Windows.Input;

namespace AdrianRobot;

public class ConsoleViewModel: ViewModelBase<ConsoleViewModel>
{
    #region Private fields

    private string text = string.Empty;

    #endregion

    #region Services

    public IProgramsExecutionService ProgramsExecutionService { get; }
    
    #endregion

    public ConsoleViewModel(IProgramsExecutionService programsExecutionService)
    {
        ProgramsExecutionService = programsExecutionService ?? throw new ArgumentNullException(nameof(programsExecutionService));
        ClearConsoleCommand = Commands.NewCommand(() => Text = string.Empty);

        ProgramsExecutionService.CommandExecutedEvent += HandleCommandExecuted;
    }

    private void HandleCommandExecuted(object? sender, CommandExecutedEventArgs e)
    {
        Text += e.Command + "\n";
    }

    #region Public Fieds

    public string Text { get => text; set => Set(ref text, value); }
    public ICommand ClearConsoleCommand { get; set; }

    #endregion
}
