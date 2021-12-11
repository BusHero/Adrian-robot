using AdrianRobot.Domain;

using System.Collections.Immutable;

namespace AdrianRobot.Tests;

public class MainViewModelTests
{
    [Fact]
    public void Programs_AtTheBeginning_AreEmpty()
    {
        var config = new Config(Array.Empty<string>());

        var mainViewModel = new MainViewModel(config.ProgramsService);
        
        mainViewModel.Programs.Should().BeEmpty();
    }

    [Fact]
    public void ProgramsComesFromTheProgramsService()
    {
        var config = new Config();

        var mainViewModel = new MainViewModel(config.ProgramsService);
        
        mainViewModel.Programs
            .Select(program => program.Name)
            .Should()
            .BeEquivalentTo(config.ProgramNames);
    }

    [Fact]
    public void FirstProgramShouldBeSelected()
    {
        var config = new Config();
        
        var mainViewModel = new MainViewModel(config.ProgramsService);

        mainViewModel.Programs[0].IsSelected.Should().Be(true);
        mainViewModel.Selected.Should()
            .Match<ProgramOverviewViewModel>(x => x.Program == mainViewModel.Programs[0].Program);
    }

    [Fact]
    public void OnlyOneProgramIsSelected()
    {
        var config = new Config();

        var mainViewModel = new MainViewModel(config.ProgramsService);
        mainViewModel.Programs[1].IsSelected = true;

        mainViewModel.Programs
            .Select(program => program.IsSelected)
            .Should()
            .BeEquivalentTo(new[] { false, true, false });

        mainViewModel.Selected.Should()
            .Match<ProgramOverviewViewModel>(x => x.Program == mainViewModel.Programs[1].Program);
    }

    [Fact]
    public void CreateNewProgram()
    {
        var config = new Config();
        var foo = config.ProgramNames.Select((name, i) => (i == 0, name));

        var mainViewModel = new MainViewModel(config.ProgramsService);

        mainViewModel.CreateNewProgram();

        mainViewModel.Programs.Select(program => (program.IsSelected, program.Name))
            .Should()
            .BeEquivalentTo(foo.Append((false, "New Program")));
    }

    [Fact]
    public void WhenSettingsToggleIsSetProgramsAreUnselected()
    {
        var config = new Config();

        var mainViewModel = new MainViewModel(config.ProgramsService)
        {
            IsSettingsSelected = true
        };

        mainViewModel.IsSettingsSelected.Should().BeTrue();
        mainViewModel.Programs.Select(program => program.IsSelected).Should().BeEquivalentTo(new[] { false, false, false });
        mainViewModel.Selected.Should().BeOfType<SettingsViewModel>();
    }

    [Fact]
    public void WhenProgramIsSelected_SettingsIsUnselected()
    {
        var config = new Config();

        var mainViewModel = new MainViewModel(config.ProgramsService)
        {
            IsSettingsSelected = true
        };

        mainViewModel.Programs[0].IsSelected = true;

        mainViewModel.IsSettingsSelected.Should().BeTrue();
        mainViewModel.Programs.Select(program => program.IsSelected).Should().BeEquivalentTo(new[] { true, false, false });
        mainViewModel.Selected.Should().BeOfType<ProgramOverviewViewModel>();
    }
}

public class Config
{
    public Config(IEnumerable<string> programNames)
    {
        ProgramNames = programNames.ToImmutableList();
        Programs = ProgramNames
            .Select(program => new Program(new ProgramId(), program, 0))
            .ToImmutableList();

        ProgramsService = Substitute.For<IProgramsService>();
        ProgramsService.GetAllPrograms().Returns(Programs);
        ProgramsService.CreateProgram("New Program").Returns(new Program(new ProgramId(), "New Program", 0));
    }

    public Config(): this(new[] { "first", "second", "third" }) { }

    public ImmutableList<Program> Programs { get; }
    public ImmutableList<string> ProgramNames { get; }
    public IProgramsService ProgramsService { get; }
}