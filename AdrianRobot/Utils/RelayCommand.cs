using System.Windows.Input;

namespace AdrianRobot;

internal class RelayCommand : ICommand
{
    public string Name { get; }

    public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute, string? name = default)
    {
        ExecuteAction = execute ?? throw new ArgumentNullException(nameof(execute));
        CanExecuteFunc = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        Name = name ?? $"{nameof(RelayCommand)}.{Counter.Next()}";
    }

    private Action<object?> ExecuteAction { get; }
    private Func<object?, bool> CanExecuteFunc { get; }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => CanExecuteFunc(parameter);

    public void Execute(object? parameter) => ExecuteAction(parameter);

    private static Counter Counter { get; } = new Counter();
}

internal class Counter
{
    private int _currentValue;

    public int CurrentValue => _currentValue;

    public Counter(int startValue = 0) => _currentValue = startValue;

    public int Next() => Interlocked.Increment(ref _currentValue);
}

public static class Commands
{
    public static ICommand None { get; } = new RelayCommand(_ => { }, _ => true);

    public static ICommand NewCommand() => None;
    public static ICommand NewCommand(string name) => new RelayCommand(_ => { }, _ => true, name);

    //Execute
    public static ICommand NewCommand<TParameter>(
        Action<TParameter?> executeAction,
        string? name = default
        ) => new RelayCommand(
            parameter => executeAction(parameter == null ? default : (TParameter)parameter),
            _ => true,
            name);

    public static ICommand NewCommand(
        Action<object?> executeAction,
        string? name = default
        ) => new RelayCommand(
            executeAction,
            _ => true,
            name);

    public static ICommand NewCommand(
        Action executeAction,
        string? name = default) => new RelayCommand(
            _ => executeAction(),
            _ => true,
            name);

    public static ICommand NewCommand(
        Action<object?> executeAction,
        Func<object?, bool> canExecuteFunc,
        string? name = default) => new RelayCommand(executeAction, canExecuteFunc, name);

    public static ICommand NewCommand<TParameter>(
        Action<TParameter?> executeAction,
        Func<TParameter?, bool> canExecuteFunc,
        string? name = default) => new RelayCommand(
            parameter => executeAction(parameter is null ? default : (TParameter)parameter),
            parameter => canExecuteFunc(parameter is null ? default : (TParameter)parameter),
            name);

    public static ICommand NewCommand(
        Action executeAction,
        Func<bool> canExecuteFunc,
        string? name = default
        ) => new RelayCommand(
            _ => executeAction(),
            _ => canExecuteFunc(),
            name);
}
