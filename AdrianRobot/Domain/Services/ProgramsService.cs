﻿using System;
using System.Collections.Immutable;
using System.Linq;

namespace AdrianRobot.Domain;

public class ProgramsService : IProgramsService
{
    #region Private Services

    private IProgramsRepository ProgramsRepository { get; }
    private IPointsService PointsService { get; }

    #endregion
    
    public ProgramsService(IProgramsRepository programsRepository, IPointsService pointsService)
    {
        ProgramsRepository = programsRepository ?? throw new ArgumentNullException(nameof(programsRepository));
        PointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
    }

    #region Public Methods

    public Program CreateProgram(string productName)
    {
        var program = new Program(new ProgramId(), productName, 0, Array.Empty<Point>());
        ProgramsRepository.SaveProgram(program);
        return program;
    }

    public Option<string> GetProgramName(ProgramId programId) => from program in ProgramsRepository.GetProgram(programId)
                                                                 select program.Name;

    public ImmutableList<string> GetAllProgramNames() => ProgramsRepository
        .GetAllPrograms()
        .Select(program => program.Name)
        .ToImmutableList();

    public ImmutableList<Program> GetAllPrograms() => ProgramsRepository
        .GetAllPrograms();

    public Option<Program> GetProgram(ProgramId program) => ProgramsRepository
        .GetProgram(program);

    public void UpdateProgramName(ProgramId programId, string newProgramName)
    {
        var program = ProgramsRepository
            .GetProgram(programId)
            .Modify(program => program.Name = newProgramName);
        ProgramsRepository.SaveProgram(program);
    }

    public void RemoveProgram(ProgramId id) => ProgramsRepository.RemoveProgram(id);

    public void AddPoint(ProgramId programId, PointId pointId, int wait, int shake)
    {
        var program = ProgramsRepository
            .GetProgram(programId);
        var point = PointsService.GetPoint(pointId);
        
        var programWithPoint = program
            .Map2(point, (program, point) => program.AddPoint(point, wait, shake))
            .Select(tuple => tuple.Item1);
        
        ProgramsRepository.SaveProgram(programWithPoint);
    }
    
    #endregion
}

public static class ProgramRepositoryExtensions
{
    public static void SaveProgram(this IProgramsRepository programsRepository, Option<Program> optionalProgram)
    {
        ArgumentNullException.ThrowIfNull(programsRepository);
        ArgumentNullException.ThrowIfNull(optionalProgram);

        if (optionalProgram is Some<Program> someProgram)
            programsRepository.SaveProgram(someProgram.Value);
    }
}
