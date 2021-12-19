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

        var settingsViewModel = new SettingsViewModel(pointsService);

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

        var settingsViewModel = new SettingsViewModel(pointsService);

        settingsViewModel.Points
            .Select(viewModel => viewModel.Point)
            .Should().BeEquivalentTo(points);
    }

    [Fact]
    public void CreatePointCommand()
    {
        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoints().Returns(ImmutableList.Create<Point>());

        var settingsViewModel = new SettingsViewModel(pointsService);

        ICommand createPointCommand = settingsViewModel.AddPointCommand;
        createPointCommand.Execute(default);

        pointsService.Received().CreatePoint();
    }
}
