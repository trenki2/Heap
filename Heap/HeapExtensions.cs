/*
 *
 * HeapExtensions ported from C++ STLPort
 * (c) trenki2 2019
 *
 */

/*
 *
 *
 * Copyright (c) 1994
 * Hewlett-Packard Company
 *
 * Copyright (c) 1996,1997
 * Silicon Graphics Computer Systems, Inc.
 *
 * Copyright (c) 1997
 * Moscow Center for SPARC Technology
 *
 * Copyright (c) 1999
 * Boris Fomitchev
 *
 * This material is provided "as is", with absolutely no warranty expressed
 * or implied. Any use is at your own risk.
 *
 * Permission to use or copy this software for any purpose is hereby granted
 * without fee, provided the above notices are retained on all copies.
 * Permission to modify the code and to distribute modified code is granted,
 * provided the above notices are retained, and a notice that the code was
 * modified is included with the above copyright notice.
 *
 */

using System;
using System.Collections.Generic;

public static class HeapExtensions
{
    /// <summary>
    /// Constructs a max heap for the entire list.
    /// </summary>
    public static void MakeHeap<T>(this IList<T> data) where T : IComparable
    {
        MakeHeap(data, 0, data.Count);
    }

    /// <summary>
    /// Constructs a max heap in the range [first, last).
    /// </summary>
    public static void MakeHeap<T>(this IList<T> data, int first, int last) where T : IComparable
    {
        if (last - first < 2)
            return;
        int len = last - first;
        int parent = (len - 2) / 2;

        for (; ; )
        {
            AdjustHeap(data, first, parent, len, data[first + parent]);
            if (parent == 0)
                return;
            parent--;
        }
    }

    /// <summary>
    /// Inserts the last element of the list into the max heap defined by the
    /// range[first, last - 1).
    /// </summary>
    public static void PushHeap<T>(this IList<T> data, int first, int last) where T : IComparable
    {
        PushHeap(data, first, (last - first) - 1, 0, data[last - 1]);
    }

    /// <summary>
    /// Equivalent to PushHeap(data, 0, data.Count).
    /// </summary>
    public static void PushHeap<T>(this IList<T> data) where T : IComparable
    {
        PushHeap(data, 0, data.Count);
    }

    /// <summary>
    /// Swaps the value in the position first and the value in the position
    /// last-1 and makes the subrange [first, last-1) into a heap. This has the
    /// effect of removing the first element from the heap defined by the range
    /// [first, last).
    /// </summary>
    public static void PopHeap<T>(this IList<T> data, int first, int last) where T : IComparable
    {
        var result = data[first];
        var val = data[last - 1];
        AdjustHeap(data, first, 0, last - 1 - first, val);
        data[last - 1] = result;
    }

    /// <summary>
    /// Equivalent to PopHeap(data, 0, data.Count).
    /// </summary>
    public static void PopHeap<T>(this IList<T> data) where T : IComparable
    {
        PopHeap(data, 0, data.Count);
    }

    /// <summary>
    /// Use HeapSort to sort the list.
    /// </summary>
    public static void HeapSort<T>(this IList<T> data) where T : IComparable
    {
        data.MakeHeap();
        for (int i = data.Count; i > 0; i--)
            data.PopHeap(0, i);
    }

    private static void PushHeap<T>(IList<T> data, int first, int holeIndex, int topIndex, T val) where T : IComparable
    {
        int parent = (holeIndex - 1) / 2;

        while (holeIndex > topIndex && data[first + parent].CompareTo(val) < 0)
        {
            data[first + holeIndex] = data[first + parent];
            holeIndex = parent;
            parent = (holeIndex - 1) / 2;
        }

        data[first + holeIndex] = val;
    }

    private static void AdjustHeap<T>(IList<T> data, int first, int holeIndex, int len, T val) where T : IComparable
    {
        int topIndex = holeIndex;
        int secondChild = 2 * holeIndex + 2;

        while (secondChild < len)
        {
            if (data[first + secondChild].CompareTo(data[first + (secondChild - 1)]) < 0)
                secondChild--;
            data[first + holeIndex] = data[first + secondChild];
            holeIndex = secondChild;
            secondChild = 2 * (secondChild + 1);
        }

        if (secondChild == len)
        {
            data[first + holeIndex] = data[first + (secondChild - 1)];
            holeIndex = secondChild - 1;
        }

        PushHeap(data, first, holeIndex, topIndex, val);
    }
}