using AdrianRobot.Domain;

using Microsoft.Extensions.DependencyInjection;

using System.Windows;

namespace AdrianRobot;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IProgramsRepository>(_ => new InMemoryProgramsRepository("First", "Second", "Third"))
            .AddSingleton<IProgramsService, ProgramsService>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<Window>(services => new MainWindow
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            })
            .BuildServiceProvider();

        var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();

        MainWindow = serviceProvider.GetRequiredService<Window>();
        MainWindow.Show();
    }
}
