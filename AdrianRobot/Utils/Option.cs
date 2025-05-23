﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdrianRobot;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "I know what I am doing.")]
public interface Option<out T> { }

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "I know what I am doing.")]
public interface None<out T> : Option<T> { }

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "I know what I am doing.")]
public interface Some<out T> : Option<T>
{
    public T Value { get; }
}

public static class Option
{
    internal record SomeInstance<T>(T Value) : Some<T> { }
    internal record NoneInstance<T>() : None<T>
    {
        public static NoneInstance<T> Default { get; } = new NoneInstance<T>();
    }

    public static Option<T> Some<T>(T item) => new SomeInstance<T>(item);

    public static Option<T> None<T>() => NoneInstance<T>.Default;

    public static Option<T> ToOption<T>(this T? item) => item switch
    {
        null => None<T>(),
        _ => Some<T>(item)
    };

    public static Option<U> Select<T, U>(this Option<T> option, Func<T, U> map) => option switch
    {
        Some<T> { Value: var value } => Some(map(value)),
        _ => None<U>()
    };

    public static Option<T> Modify<T>(this Option<T> option, Action<T> modifier)
    {
        if (option is Some<T> { Value: var value })
        {
            modifier(value);
            return value.ToOption();
        }
        return option;
    }

    public static Option<U> SelectMany<T, U>(this Option<T> option, Func<T, Option<U>> selector) => option switch
    {
        Some<T> { Value: var value } => selector(value).Select(x => x),
        _ => None<U>()
    };

    public static Option<V> SelectMany<T, U, V>(this Option<T> option, Func<T, Option<U>> selector, Func<T, U, V> projector) => option switch
    {
        Some<T> { Value: var value } => selector(value).Select(u => projector(value, u)),
        _ => None<V>()
    };

    public static Option<V> Map2<T, U, V>(this Option<T> first, Option<U> second, Func<T, U, V> map) => (first, second, map) switch
    {
        (null, _, _) => throw new ArgumentNullException(nameof(first)),
        (_, null, _) => throw new ArgumentNullException(nameof(second)),
        (_, _, null) => throw new ArgumentNullException(nameof(map)),
        (Some<T> { Value: var firstValue }, Some<U> { Value: var secondValue }, _) => map(firstValue, secondValue).ToOption(),
        _ => None<V>()
    };

    public static Option<(T, U)> Map2<T, U>(this Option<T> first, Option<U> second, Action<T, U> action) => (first, second, action) switch
    {
        (null, _, _) => throw new ArgumentNullException(nameof(first)),
        (_, null, _) => throw new ArgumentNullException(nameof(second)),
        (_, _, null) => throw new ArgumentNullException(nameof(action)),
        _ => first.Map2(second, (first, second) => 
        {
            action(first, second);
            return (first, second);
        })
    };

    public static Option<T> Combine<T>(this Option<T> first, Option<T> second, Func<T, T, T> map) => (first, second, map) switch
    {
        (null, _, _) => throw new ArgumentNullException(nameof(first)),
        (_, null, _) => throw new ArgumentNullException(nameof(second)),
        (_, _, null) => throw new ArgumentNullException(nameof(map)),
        (Some<T> { Value: var firstValue }, Some<T> { Value: var secondValue }, _) => map(firstValue, secondValue).ToOption(),
        (Some<T>, _, _) => first,
        (_, Some<T>, _) => second,
        _ => None<T>()
    };

    public static Option<T> Binary<T>(this Option<T> first, Option<T> second, Func<T, T, T> map) => first.Map2(second, map);

    public static Func<Option<T>, Option<U>, Option<V>> Map2<T, U, V>(Func<T, U, V> map) => (first, second) => first.Map2(second, map);

    public static Func<Option<T>, Option<T>, Option<T>> Binary<T>(Func<T, T, T> map) => Map2(map);

    public static T ValueOrDefault<T>(this Option<T> option, T @default) => option switch
    {
        null => throw new ArgumentNullException(nameof(option)),
        Some<T> { Value: var value } => value,
        _ => @default
    };

    public static Option<T> IfNone<T>(this Option<T> option, Func<Option<T>> @default) => option switch
    {
        Some<T> => option,
        _ => @default()
    };


    public static Option<T> FirstIdentity<T>() => None<T>();

    public static Option<T> FindFirst<T>(this Option<T> first, Option<T> second) => first switch
    {
        Some<T> => first,
        _ => second
    };

    public static Option<T> Accumulate<T>(IEnumerable<Option<T>> options) => options
        .Aggregate(FirstIdentity<T>(), FindFirst);
}