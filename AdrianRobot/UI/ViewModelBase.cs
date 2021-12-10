using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AdrianRobot;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    protected virtual void HandlePropertyChangedEvent(
        object sender,
        PropertyChangedEventArgs e)
    {
        if (false == HandlersDictionary.TryGetValue(e.PropertyName, out var list))
            return;

        foreach (var (foo, _) in list.Where(tuple => tuple.Item2(sender, e)))
        {
            foo?.Invoke(sender, e);
        }
    }

    public ViewModelBase() => PropertyChanged += HandlePropertyChangedEvent;

    public event PropertyChangedEventHandler PropertyChanged;

    protected void FirePropertyChangedEvent(
        [CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected virtual bool Set<T>(
        ref T storage,
        T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;
        storage = value;
        FirePropertyChangedEvent(propertyName);
        return true;
    }

    protected virtual bool Set<T>(object source, T value, string propertyName)
    {
        var property = source.GetType()
            .GetRuntimeProperties()
            .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
        if (property?.PropertyType != typeof(T))
            return false;
        if (EqualityComparer<T>.Default.Equals((T)property.GetValue(source), value))
            return false;
        property.SetValue(source, value);
        FirePropertyChangedEvent(propertyName);
        return true;
    }

    #region SubscribePropertyChanged

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        Action eventHandler) => SubscribePropertyChanged(propertyName, (_, __) => eventHandler(), AlwaysTrue);


    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler eventHandler) => SubscribePropertyChanged(propertyName, eventHandler, AlwaysTrue);
    
    


    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        Action eventHandler,
        Func<bool> canChange) => SubscribePropertyChanged(propertyName, (_, __) => eventHandler(), (_, __) => canChange());

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        Action eventHandler,
        CanNotifyPropertyChanged canChange) => SubscribePropertyChanged(propertyName, (_, __) => eventHandler(), canChange);

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler eventHandler,
        Func<bool> canChange) => SubscribePropertyChanged(propertyName, eventHandler, (_, __) => canChange());

    public virtual ViewModelBase SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler eventHandler,
        CanNotifyPropertyChanged canChange)
    {
        if (string.IsNullOrEmpty(propertyName))
            throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty", nameof(propertyName));

        if (eventHandler is null)
            throw new ArgumentNullException(nameof(eventHandler));

        if (canChange is null)
            throw new ArgumentNullException(nameof(canChange));

        if (HandlersDictionary.TryGetValue(propertyName, out var list))
            list.Add((eventHandler, canChange));
        else
            HandlersDictionary[propertyName] = new List<(PropertyChangedEventHandler, CanNotifyPropertyChanged)>
            {
                (eventHandler, canChange)
            };

        return this;
    }

    #endregion

    #region Private Fields

    private Dictionary<string, List<(PropertyChangedEventHandler, CanNotifyPropertyChanged)>> HandlersDictionary { get; } =
        new Dictionary<string, List<(PropertyChangedEventHandler, CanNotifyPropertyChanged)>>();

    #endregion

    protected NamedViewModel<TItem> ToViewModel<TItem>(string name, TItem item) => new NamedViewModel<TItem>(name, item);

    public class NamedViewModel<TItem>
    {
        public string Name { get; set; }
        public TItem Item { get; set; }

        public NamedViewModel(string name, TItem item)
        {
            Name = name;
            Item = item;
        }
    }

    private static readonly CanNotifyPropertyChanged AlwaysTrue = (_, __) => true;

    public delegate bool CanNotifyPropertyChanged(object sender, PropertyChangedEventArgs e);

    public class NoneViewModelBase : ViewModelBase { }

    public static ViewModelBase Empty { get; } = new NoneViewModelBase();

    public static NamedViewModel<TItem> NewNamedViewModel<TItem>(string name, TItem item) => new(name, item);
}

public delegate void PropertyChangedEventHandler<T>(T? sender, PropertyChangedEventArgs e);


public abstract class ViewModelBase<T> : ViewModelBase 
    where T : ViewModelBase<T>
{
    public virtual T? SubscribePropertyChanged(
        string propertyName,
        Action<T> eventHandler) => SubscribePropertyChanged(propertyName, (sender, _) => { eventHandler(sender); });

    public virtual T? SubscribePropertyChanged(
        string propertyName,
        PropertyChangedEventHandler<T> eventHandler)
    {
        var foo = base.SubscribePropertyChanged(propertyName, (sender, eventArgs) =>
        {
            if (sender is T t)
                eventHandler(t, eventArgs);
        });
        return foo as T;
    }
}