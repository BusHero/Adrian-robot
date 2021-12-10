using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdrianRobot;

public class MockViewModel
{
    public static MockViewModel Instance { get; } = new MockViewModel();

    public  ObservableCollection<int> Data { get; } = new ObservableCollection<int>() { 1, 2, 3 };
}
