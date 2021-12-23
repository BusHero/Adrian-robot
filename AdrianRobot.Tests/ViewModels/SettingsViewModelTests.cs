using System.Collections.Immutable;
using System.Windows.Input;

using AdrianRobot.Domain;

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
        var point = new Point(new(), "", 0, 0);
        var pointsService = Substitute.For<IPointsService>();
        pointsService.GetPoints().Returns(ImmutableList.Create<Point>());
        pointsService.CreatePoint().Returns(point);

        var settingsViewModel = new SettingsViewModel(pointsService, Substitute.For<ISettingsRepository>());

        settingsViewModel.AddPointCommand.Execute(default);

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

        _ = settingsViewModel.Motor1Speed.Should().Be(10);
        _ = settingsViewModel.Motor2Speed.Should().Be(11);
    }

    [Fact]
    public void ValuesAreUpdatedInSettingsRepository()
    {
        var settingsRepository = new InMemorySettingsRepository();

        var pointsService = Substitute.For<IPointsService>();
        _ = pointsService.GetPoints().Returns(ImmutableList.Create<Point>());

        var settingsViewModel = new SettingsViewModel(pointsService, settingsRepository)
        {
            Motor1Speed = 10,
            Motor2Speed = 11
        };

        _ = settingsRepository.Motor1Speed.Should().Be(10);
        _ = settingsRepository.Motor2Speed.Should().Be(11);
    }

    [Fact]
    public void UpdatePoint()
    {
        var repository = new InMemoryPointsRepository();
        Point point = new(new(), "first", 10, 100);
        repository.SavePoint(point);

        var pointsService = new PointsService(repository);
        var settingsViewModel = new SettingsViewModel(pointsService, Substitute.For<ISettingsRepository>());
        settingsViewModel.Points[0].Name = "asd";
        settingsViewModel.Points[0].MotorYPosition = 123;
        settingsViewModel.Points[0].MotorZPosition = 321;

        _ = repository.GetPoint(point.Id).Select(point => point.Name).Should().Be("asd".ToOption());
        _ = repository.GetPoint(point.Id).Select(point => point.MotorYPosition).Should().Be(123.ToOption());
        _ = repository.GetPoint(point.Id).Select(point => point.MotorZPosition).Should().Be(321.ToOption());
    }
}

