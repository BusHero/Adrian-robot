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
            .AddSingleton<IPointsRepository>(_ => new InMemoryPointsRepository())
            .AddSingleton<IPointsService, PointsService>()
            .AddSingleton<IProgramsRepository>(_ => new InMemoryProgramsRepository())
            .AddSingleton<IProgramsService, ProgramsService>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<Window>(services => new MainWindow
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            })
            .BuildServiceProvider();

        MainWindow = serviceProvider.GetRequiredService<Window>();
        MainWindow.Show();
    }
}
