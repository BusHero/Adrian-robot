namespace AdrianRobot;

public static class Lists
{
    public static List<T> Of<T>(params T[] items) => new(items);
}
