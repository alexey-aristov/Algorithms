using System;
using System.Collections.Generic;
using System.Linq;

namespace Aristov.Sort
{
    public interface ILog
    {
        void IterationHeader(int iteration);
        void IterationInfo<T>(IEnumerable<T> source, string prefixFormat = "", params object[] args);
    }

    public class LogEmpty : ILog
    {
        public void IterationHeader(int iteration)
        {}

        public void IterationInfo<T>(IEnumerable<T> source, string prefixFormat, params object[] args)
        {}
    }

    public class LogConsole : ILog
    {
        public void IterationHeader(int iteration)
        {
            Console.WriteLine($"-----------------iteration {iteration}---------------------");
        }

        public void IterationInfo<T>(IEnumerable<T> source, string prefixFormat, params object[] args)
        {
            Console.WriteLine(source.Aggregate(string.Format(prefixFormat, args), (s, i1) => $"{s} {i1},").TrimEnd(','));
        }
    }
}

