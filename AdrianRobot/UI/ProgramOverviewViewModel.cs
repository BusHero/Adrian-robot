using AdrianRobot.Domain;

using System;
using System.Collections.ObjectModel;

namespace AdrianRobot;

public class ProgramOverviewViewModel : ViewModelBase<ProgramOverviewViewModel>
{
    #region Private Services
    
    private IProgramsService ProgramsService { get; }

    #endregion

    #region Properties

    public Program Program { get; private set; }

    public ObservableCollection<PointViewModel> Points { get; }
    
    public string Name => Program.Name;
    
    public int Repeats => Program.Repeats;

    #endregion

    public ProgramOverviewViewModel(Program program, IProgramsService programsService)
    {
        Program = program ?? throw new ArgumentNullException(nameof(program));
        ProgramsService = programsService ?? throw new ArgumentNullException(nameof(programsService));
        Points = program
            .Points
            .Select(point => new PointViewModel(point))
            .ToObservableCollection();
    }

    #region Public Methods

    public void UpdateRepeats(int repeats)
    {
        ProgramsService.UpdateProgramRepeats(Program.Id, repeats);
        Program = ProgramsService.GetProgram(Program.Id).ValueOrDefault(Program.Default);
    }

    public void UpdateName(string newProgramName)
    {
        ProgramsService.UpdateProgramName(Program.Id, newProgramName);
        Program = ProgramsService.GetProgram(Program.Id).ValueOrDefault(Program.Default);
    }

    #endregion
}
