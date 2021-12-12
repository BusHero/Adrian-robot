﻿
using AdrianRobot.Domain;

using System.Collections.Immutable;

namespace AdrianRobot.Tests;

public class ProgramOverviewViewModelTests
{
    [Fact]
    public void ProgramOverviewHasAName()
    {
        var programName = "Program name";
        var program = new Program(new(), programName, 0, Array.Empty<Point>());
        var sut = new ProgramOverviewViewModel(program, Substitute.For<IProgramsService>());

        sut.Name.Should().Be(programName);
    }

    [Fact]
    public void ProgramOverviewShowsRepeats()
    {
        var programName = "Program name";
        var repeats = 30;
        var program = new Program(new(), programName, repeats, Array.Empty<Point>());
        var sut = new ProgramOverviewViewModel(program, Substitute.For<IProgramsService>());

        sut.Repeats.Should().Be(repeats);
    }

    [Fact]
    public void ProgramOverviewShowsPoints()
    {
        var program = new Program(
            new(),
            "Program name",
            30,
            new Point[] 
            { 
                new (new PointId(), "Point 1", 100, 100), 
                new (new PointId(), "Point", 100, 100)
            });
        var sut = new ProgramOverviewViewModel(program, Substitute.For<IProgramsService>());

        sut.Points
            .Select(point => point.Name)
            .Should()
            .BeEquivalentTo(new[] 
            { 
                "Point 1 (y: 100, z: 100)", 
                "Point (y: 100, z: 100)" 
            });
    }

    [Fact]
    public void ModifyName()
    {
        const string newProgramName = "New Name";
        var programsService = Substitute.For<IProgramsService>();
        ProgramId programId = new();
        var program = new Program(
            programId,
            "Program name",
            30,
            Array.Empty<Point>());
        programsService.GetProgram(programId).Returns(new Program(
            programId,
            newProgramName,
            30,
            Array.Empty<Point>()).ToOption());

        var sut = new ProgramOverviewViewModel(program, programsService);

        sut.UpdateName(newProgramName: newProgramName);

        sut.Name.Should().Be(newProgramName);
        sut.Program.Name.Should().Be(newProgramName);
        programsService.Received().UpdateProgramName(program.Id, "New Name");
    }

    [Fact]
    public void ModifyRepeats()
    {
        var newRepeats = 60;
        var programsService = Substitute.For<IProgramsService>();
        ProgramId programId = new();
        var program = new Program(
            programId,
            "Program name",
            30,
            Array.Empty<Point>());
        programsService.GetProgram(programId).Returns(new Program(
            programId,
            "Program name",
            newRepeats,
            Array.Empty<Point>()).ToOption());

        var sut = new ProgramOverviewViewModel(program, programsService);

        sut.UpdateRepeats(newRepeats);

        sut.Repeats.Should().Be(newRepeats);
        sut.Program.Repeats.Should().Be(newRepeats);
        programsService.Received().UpdateProgramRepeats(program.Id, newRepeats);
    }
}

