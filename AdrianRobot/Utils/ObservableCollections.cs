using System.Collections.ObjectModel;

namespace AdrianRobot;

internal static class ObservableCollections
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumeable) => 
        new(enumeable);
}
