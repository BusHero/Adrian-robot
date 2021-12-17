using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace AdrianRobot;

public partial class ProgramOverview: ResourceDictionary
{
    public void PreviewTextInput(object sender, TextCompositionEventArgs e)
    {

    }

    private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
    private static bool IsTextAllowed(string text)
    {
        return !_regex.IsMatch(text);
    }
}
