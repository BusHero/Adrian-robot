using System.Windows.Input;

namespace AdrianRobot.Tests;

public class ConsoleViewModelTest
{
    [Fact]
    public void TextInitialyIsEmpty()
    {
        var consoleViewModel = new ConsoleViewModel(Substitute.For<IProgramsExecutionService>());
        consoleViewModel.Text.Should().BeEmpty();
    }

    [Fact]
    public void TextUpdates_CommandExecutedEvent_IsFired()
    {
        var programExecutionService = Substitute.For<IProgramsExecutionService>();

        var consoleViewModel = new ConsoleViewModel(programExecutionService);

        programExecutionService.CommandExecutedEvent += Raise.EventWith(new CommandExecutedEventArgs("Some command here and there"));
        consoleViewModel.Text.Should().Be("Some command here and there\n");
    }

    [Fact]
    public void CallingClearTextEventClearsText()
    {
        var consoleViewModel = new ConsoleViewModel(Substitute.For<IProgramsExecutionService>())
        {
            Text = "Some text here and there"
        };

        consoleViewModel.Text.Should().Be("Some text here and there");

        consoleViewModel.ClearConsoleCommand.Execute(default);

        consoleViewModel.Text.Should().BeEmpty();
    }
}
