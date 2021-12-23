using AdrianRobot.Domain;

using Microsoft.Extensions.DependencyInjection;

using System.Collections.Immutable;
using System.Windows;

namespace AdrianRobot;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceProvider = new ServiceCollection()
            .AddSingleton<ISettingsRepository, InMemorySettingsRepository>()
            .AddSingleton<IProgramsExecutionService, ProgramsExecutionService>()
            .AddSingleton<IProgramsViewModelFactory, ProgramViewModelFactory>()
            .AddSingleton<IProgramOverviewViewModelFactory, ProgramOverviewViewModelFactory>()
            .AddSingleton<IPointsRepository>(_ =>
            {
                var repository = new InMemoryPointsRepository();
                repository.SavePoint(new(new(), "Point 1", 100, 200));
                repository.SavePoint(new(new(), "Point 2", 150, 250));
                return repository;
            })
            .AddSingleton<IPointsService, PointsService>()
            .AddSingleton((Func<IServiceProvider, IProgramsRepository>)(_ =>
            {
                var repository = new InMemoryProgramsRepository();
                repository.SaveProgram(new (
                    new(),
                    "Program",
                    10,
                    Arrays.Of(
                        new Point(new(), "Point 1", 10, 20),
                        new Point(new(), "Point 2", 10, 30),
                        new Point(new(), "Point 3", 10, 10))));
                return repository;
            }))
            .AddSingleton<IProgramsService, ProgramsService>()
            .AddSingleton<MainViewModel>()
            .AddSingleton((Func<IServiceProvider, Window>)(services => new MainWindow
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            }))
            .BuildServiceProvider();

        var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();

        MainWindow = serviceProvider.GetRequiredService<Window>();
        MainWindow.Show();
    }
}
