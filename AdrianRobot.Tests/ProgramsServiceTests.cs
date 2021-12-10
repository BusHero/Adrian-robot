using AdrianRobot.Domain;

using Xunit;

namespace AdrianRobot.Tests;

public class ProgramsServiceTests
{
    [Fact]
    public void CanCreateAProgram()
    {
        var programsService = new ProgramsService();
    }
}
