
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSinusPerceptron();
            //SimpleTest();
        }


        private static void CreateSinusPerceptron()
        {
            List<Tuple<decimal, decimal>> samples = new List<Tuple<decimal, decimal>>()
            {
                new Tuple<decimal, decimal>(0.1m, (decimal) Math.Sin(0.1)),
                new Tuple<decimal, decimal>(0.2m, (decimal) Math.Sin(0.2)),
                //new Tuple<decimal, decimal>(0.3m, (decimal) Math.Sin(0.3)),
                //new Tuple<decimal, decimal>(0.4m, (decimal) Math.Sin(0.4)),
                //new Tuple<decimal, decimal>(0.5m, (decimal) Math.Sin(0.5)),
                //new Tuple<decimal, decimal>(0.6m, (decimal) Math.Sin(0.6)),
                //new Tuple<decimal, decimal>(0.7m, (decimal) Math.Sin(0.7)),
            };
            List<Tuple<decimal, decimal>> tests = new List<Tuple<decimal, decimal>>()
            {
                new Tuple<decimal, decimal>(0.1m, (decimal) Math.Sin(0.1)),
                new Tuple<decimal, decimal>(0.2m, (decimal) Math.Sin(0.2)),
                new Tuple<decimal, decimal>(0.3m, (decimal) Math.Sin(0.3)),
                new Tuple<decimal, decimal>(0.4m, (decimal) Math.Sin(0.4)),
                new Tuple<decimal, decimal>(0.5m, (decimal) Math.Sin(0.5)),
                new Tuple<decimal, decimal>(0.6m, (decimal) Math.Sin(0.6)),
                new Tuple<decimal, decimal>(0.7m, (decimal) Math.Sin(0.7)),
            };
            Perceptron p = new Perceptron(new[] { 1, 2, 5, 10, 7, 1 }, 0.1m);
            decimal delta = 0.000001m;
            decimal[] @out;
            decimal[] errors;

            for (int i = 0; i < 50000; i++)
            {
                foreach (var sample in samples)
                {
                    @out = p.EvaluateInput(new[] { sample.Item1 });
                    errors = new[] { GetError(sample.Item2, @out[0]) };
                    p.Calculate(errors);
                    p.AdjustWeights();
                }
            }


            foreach (var test in tests)
            {
                var result = p.EvaluateInput(new[] { test.Item1 });
                if (Math.Abs(result[0] - test.Item2) > delta)
                {
                    Console.WriteLine("Faled test, expeceted{0},was {1}", test.Item2, result[0]);
                }
                else
                {
                    Console.WriteLine("Success test");
                }
            }

        }

        private static void SimpleTest()
        {
            Perceptron p = new Perceptron(new[] { 1, 3, 2, 2 }, 1);
            var @out = p.EvaluateInput(new[] { 1m });
            decimal[] expected = new[] { 0.05m, 0.01m };
            var errors = new[] { GetError(expected[0], @out[0]), GetError(expected[1], @out[1]) };
            p.AdjustWeights();
            decimal delta = 0.000001m;
            int i = 0;
            while (Math.Abs(errors[0]) > delta || Math.Abs(errors[1]) > delta)
            {
                i++;
                @out = p.EvaluateInput(new[] { 1m });
                errors = new[] { GetError(expected[0], @out[0]), GetError(expected[1], @out[1]) };
                p.Calculate(errors);
                p.AdjustWeights();
            }
        }

        private static decimal GetError(decimal expected, decimal actual)
        {
            return expected - actual;
        }
    }
}
