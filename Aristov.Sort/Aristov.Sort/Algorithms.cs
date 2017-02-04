using System;
using System.Collections.Generic;
using System.Linq;

namespace Aristov.Sort
{
    public static class Algorithms
    {
        public static T[] BubbleSort<T>(this T[] source, ILog log = null) where T : IComparable
        {
            log = log ?? new LogEmpty();
            bool swapped = true;
            int n = source.Length;
            while (swapped)
            {
                int counterForDisplay = source.Length - n + 1;
                log.IterationHeader(counterForDisplay);
                log.IterationInfo(source, "{0}|", counterForDisplay);

                n--;
                swapped = false;
                for (int i = 0; i < n; i++)
                {
                    if (source[i].CompareTo(source[i + 1]) > 0)
                    {
                        source.Swap(i, i + 1);
                        swapped = true;
                        log.IterationInfo(source, "{0}.{1}|", counterForDisplay, i + 1);
                    }
                }
            }
            return source;
        }

        private static void Swap<T>(this T[] source, int index1, int index2)
        {
            T temp = source[index1];
            source[index1] = source[index2];
            source[index2] = temp;
        }

        public static T[] InsertionSort<T>(this T[] source, ILog log = null) where T : IComparable
        {
            log = log ?? new LogEmpty();
            int n = source.Length;
            for (int i = 0; i < n; i++)
            {
                log.IterationHeader(i + 1);
                T newEl = source[i];
                int location = i - 1;
                int counter = 1;
                while (location >= 0 && source[location].CompareTo(newEl) > 0)
                {
                    log.IterationInfo(source, "{0}.{1}|", i + 1, counter);
                    source[location + 1] = source[location];
                    location--;
                    counter++;
                }
                source[location + 1] = newEl;
                log.IterationInfo(source, "{0}.{1}|", i + 1, counter);
            }
            return source;
        }

        public static List<int> RadixSort(this List<int> source, ILog log = null)
        {
            log = log ?? new LogEmpty();
            int n = source.Count;
            int shift = 1;
            var buckets = new List<List<int>>();
            int iteration = 0;

            for (int j = 0; j < 10; j++)
            {
                buckets.Add(new List<int>());
            }

            while (true)
            {
                iteration++;
                log.IterationHeader(iteration);

                for (int entry = 0; entry < n; entry++)
                {
                    var bucketNumber = source[entry] / shift % 10;
                    buckets[bucketNumber].Add(source[entry]);
                }

                source = new List<int>(n);
                int b = 1;

                foreach (var bucket in buckets)
                {
                    log.IterationInfo(bucket, "{0,2}|", b);
                    source.AddRange(bucket);
                    b++;
                }
                if (buckets.Skip(1).All(a => a.Count == 0))
                {
                    return buckets[0];
                }
                shift *= 10;

                for (int j = 0; j < 10; j++)
                {
                    buckets[j] = new List<int>();
                }
            }
        }

        public static T[] QuickSort<T>(this T[] source, ILog log = null) where T : IComparable
        {
            log = log ?? new LogEmpty();
            return QuickSortInternal<T>(source, 0, source.Length, log);
        }
        private static T[] QuickSortInternal<T>(this T[] source, int first, int last, ILog log) where T : IComparable
        {
            log.IterationHeader($"from {first} to {last}");
            log.IterationInfo(source);
            if (first < last)
            {
                int pivot = PivotList(source, first, last, log);
                source.QuickSortInternal(first, pivot, log);
                source.QuickSortInternal(pivot + 1, last, log);
            }
            return source;
        }

        private static int PivotList<T>(this T[] source, int first, int last, ILog log) where T : IComparable
        {
            var pivotPoint = first;
            var pivotValue = source[first];

            for (int i = first + 1; i < last; i++)
            {
                if (source[i].CompareTo(pivotValue) < 0)
                {
                    pivotPoint++;
                    source.Swap(pivotPoint, i);
                    log.IterationInfo(source, "pivot({0}-{1}),{2}|", first, last, i);
                }
            }
            source.Swap(first, pivotPoint);
            log.IterationInfo(source, "pivot({0}-{1})|", first, last);
            log.Line($"pivot({first}-{last})={pivotValue}");
            return pivotPoint;
        }
    }
}
