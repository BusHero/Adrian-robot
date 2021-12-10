using AdrianRobot.Domain;

using FluentAssertions;

using NSubstitute;

using System.Collections.Immutable;
using System.Linq;

using Xunit;

namespace AdrianRobot.Tests;

public class MainViewModelTests
{
    [Fact]
    public void Programs_AtTheBeginning_AreEmpty()
    {
        var programsService = Substitute.For<IProgramsService>();
        programsService.GetAllPrograms().Returns((ImmutableList<Program>?)ImmutableList<Program>.Empty);

        var mainViewModel = new MainViewModel(programsService);
        mainViewModel.Programs.Should().BeEmpty();
    }

    [Fact]
    public void ProgramsComesFromTheProgramsService()
    {
        var programNames = ImmutableList.Create("first", "second", "third");
        var programs = programNames
            .Select(program => new Program(new ProgramId(), program))
            .ToImmutableList();

        var programsService = Substitute.For<IProgramsService>();
        programsService.GetAllPrograms().Returns(programs);

        var mainViewModel = new MainViewModel(programsService);
        
        mainViewModel.Programs
            .Select(program => program.Name)
            .Should()
            .BeEquivalentTo(programNames);
    }

    [Fact]
    public void FirstProgramShouldBeSelected()
    {
        var programNames = ImmutableList.Create("first", "second", "third");
        var programs = programNames
            .Select(program => new Program(new ProgramId(), program))
            .ToImmutableList();

        var programsService = Substitute.For<IProgramsService>();
        programsService.GetAllPrograms().Returns(programs);

        var mainViewModel = new MainViewModel(programsService);

        mainViewModel.Programs[0].IsSelected.Should().Be(true);
        mainViewModel.Selected.Should()
            .Match<ProgramOverviewViewModel>(x => x.Program == mainViewModel.Programs[0].Program);
    }

    [Fact]
    public void OnlyOneProgramIsSelected()
    {
        var programNames = ImmutableList.Create("first", "second", "third");
        var programs = programNames
            .Select(program => new Program(new ProgramId(), program))
            .ToImmutableList();

        var programsService = Substitute.For<IProgramsService>();
        programsService.GetAllPrograms().Returns(programs);

        var mainViewModel = new MainViewModel(programsService);
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
        var programNames = ImmutableList.Create("first", "second", "third");
        var foo = programNames.Select((name, i) => (i == 0, name));

        var programs = programNames
            .Select(program => new Program(new ProgramId(), program))
            .ToImmutableList();

        var programsService = Substitute.For<IProgramsService>();
        programsService.GetAllPrograms().Returns(programs);
        programsService.CreateProgram("New Program").Returns(new Program(new ProgramId(), "New Program"));

        var mainViewModel = new MainViewModel(programsService);

        mainViewModel.CreateNewProgram();

        mainViewModel.Programs.Count.Should().Be(4);

        mainViewModel.Programs.Select(program => (program.IsSelected, program.Name))
            .Should()
            .BeEquivalentTo(foo.Append((false, "New Program")));
    }
}
