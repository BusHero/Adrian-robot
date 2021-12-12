using AdrianRobot.Domain;

using System;
using System.Collections.ObjectModel;

namespace AdrianRobot;

public class ProgramOverviewViewModel : ViewModelBase<ProgramOverviewViewModel>
{
    #region Properties

    public Program Program { get; }

    public ObservableCollection<PointViewModel> Points { get; }
    
    public string Name => Program.Name;
    
    public int Repeats => Program.Repeats;

    #endregion

    public ProgramOverviewViewModel(Program program)
    {
        Program = program ?? throw new ArgumentNullException(nameof(program));

        Points = program
            .Points
            .Select(point => new PointViewModel(point))
            .ToObservableCollection();
    }

    #region Public Methods

    public void UpdateRepeats(int v)
    {
        throw new NotImplementedException();
    }
    
    #endregion
}
