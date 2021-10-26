using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CollectionExtensions
{
    public static int Random(this (int min, int max) collection) => UnityEngine.Random.Range(collection.min, collection.max);
    public static float Random(this (float min, float max) collection) => UnityEngine.Random.Range(collection.min, collection.max);

    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection)
            action?.Invoke(item);
    }

    public static bool AddUnique<T>(this IList<T> list, T value)
    {
        if (list.Contains(value))
            return false;
        list.Add(value);
        return true;
    }

    public static int AddUnique<T>(this IList<T> list, IEnumerable<T> values)
    {
        int q = 0;
        foreach (var value in values)
        {
            if (list.Contains(value))
                continue;
            list.Add(value);
            q++;
        }
        return q;
    }

    public static int RemoveaAll<T>(this LinkedList<T> list, Func<T, bool> predcate)
    {
        if (predcate == null || list.Count <= 0)
            return 0;

        var removed = 0;
        var next = list.First;
        while (next != null)
        {
            var current = next;
            next = next.Next;

            var value = current.Value;
            if (predcate.Invoke(value))
            {
                list.Remove(current);
                removed++;
            }
        }
        return removed;
    }

    public static T Random<T>(this IList<T> list, System.Random rnd = null)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        if (list.Count <= 0)
            throw new ArgumentException(nameof(list));

        var index = rnd == null
            ? UnityEngine.Random.Range(0, list.Count)
            : rnd.Next(0, list.Count);
        return list[index];
    }

    public static T Random<T>(this IEnumerable<T> enumerable, System.Random rnd = null)
    {
        if (enumerable == null)
            throw new ArgumentNullException(nameof(enumerable));

        if (!enumerable.Any())
            throw new ArgumentException(nameof(enumerable));

        var count = enumerable.Count();

        var index = rnd == null
            ? UnityEngine.Random.Range(0, count)
            : rnd.Next(0, count);
        return enumerable.ElementAt(index);
    }

    public static T RandomOrDefault<T>(this IList<T> list, System.Random rnd = null)
    {
        if (list == null || list.Count <= 0)
            return default(T);
        else
            return list.Random(rnd);
    }
    public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, System.Random rnd = null)
    {
        if (enumerable == null || !enumerable.Any())
            return default(T);
        else
            return enumerable.Random(rnd);
    }

    public static T[] Shuffle<T>(this IList<T> list, System.Random rnd = null)
    {
        return list
            .OrderBy(i => rnd == null
                ? UnityEngine.Random.Range(int.MinValue, int.MaxValue)
                : rnd.Next()
            )
            .ToArray();
    }
}