using System;
using System.Collections.Generic;
using System.Linq;

namespace Aristov.Sort
{
    public interface ILog
    {
        void IterationHeader(int iteration);
        void IterationHeader(string iterationInfo);
        void IterationInfo<T>(IEnumerable<T> source, string prefixFormat = "", params object[] args);
        void Line(string line);
    }

    public class LogEmpty : ILog
    {
        public void IterationHeader(int iteration)
        {}

        public void IterationInfo<T>(IEnumerable<T> source, string prefixFormat, params object[] args)
        {}

        public void Line(string line)
        {}

        public void IterationHeader(string iterationInfo)
        {}
    }

    public class LogConsole : ILog
    {
        public void IterationHeader(int iteration)
        {
            IterationHeader(iteration.ToString());
        }

        public void IterationInfo<T>(IEnumerable<T> source, string prefixFormat, params object[] args)
        {
            Console.WriteLine(source.Aggregate(string.Format(prefixFormat, args), (s, i1) => $"{s} {i1},").TrimEnd(','));
        }

        public void Line(string line)
        {
            Console.WriteLine(line);
        }

        public void IterationHeader(string iterationInfo)
        {
            Console.WriteLine($"-----------------iteration {iterationInfo}---------------------");
        }
    }
}

