using System.Collections.Immutable;
using System.Windows.Input;

namespace AdrianRobot.Tests;

public class SettingsViewModelTests
{
    [Fact]
    public void DefaultValues()
    {
        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoints().Returns(ImmutableList.Create<Point>());

        var settingsViewModel = new SettingsViewModel(pointsService, Substitute.For<ISettingsRepository>());

        settingsViewModel.Motor1Speed.Should().Be(0);
        settingsViewModel.Motor2Speed.Should().Be(0);
        settingsViewModel.Points.Should().BeEmpty();
    }

    [Fact]
    public void SettingsViewModelShowPointsFromPointsService()
    {
        var points = ImmutableList.Create<Point>(
            new (new(), "first", 10, 100),
            new (new(), "second", 11, 101));

        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoints().Returns(points);

        var settingsViewModel = new SettingsViewModel(pointsService, Substitute.For<ISettingsRepository>());

        settingsViewModel.Points
            .Select(viewModel => viewModel.Point)
            .Should().BeEquivalentTo(points);
    }

    [Fact]
    public void CreatePointCommand()
    {
        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoints().Returns(ImmutableList.Create<Point>());

        var settingsViewModel = new SettingsViewModel(pointsService, Substitute.For<ISettingsRepository>());

        ICommand createPointCommand = settingsViewModel.AddPointCommand;
        createPointCommand.Execute(default);

        pointsService.Received().CreatePoint();
    }

    [Fact]
    public void ValuesComesFromSettings()
    {
        var settingsRepository = Substitute.For<ISettingsRepository>();
        settingsRepository.Motor1Speed.Returns(10);
        settingsRepository.Motor2Speed.Returns(11);

        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoints().Returns(ImmutableList.Create<Point>());

        var settingsViewModel = new SettingsViewModel(pointsService, settingsRepository);

        settingsViewModel.Motor1Speed.Should().Be(10);
        settingsViewModel.Motor2Speed.Should().Be(11);
    }

    [Fact]
    public void ValuesAreUpdatedInSettingsRepository()
    {
        var settingsRepository = new InMemorySettingsRepository();

        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoints().Returns(ImmutableList.Create<Point>());

        var settingsViewModel = new SettingsViewModel(pointsService, settingsRepository)
        {
            Motor1Speed = 10,
            Motor2Speed = 11
        };

        settingsRepository.Motor1Speed.Should().Be(10);
        settingsRepository.Motor2Speed.Should().Be(11);
    }

    [Fact]
    public void UpdatePoint()
    {

    }
}

