
using System;

namespace Perceptron
{
    class Program
    {
        static void Main(string[] args)
        {
            Perceptron p = new Perceptron(new[] { 3, 7,3, 2 });
            var @out = p.EvaluateInput(new[] { 1m,0.1m,0.7m }); //,0.2m });
            decimal[] expected = new[] { 0.05m, 0.01m };
            decimal delta = 0.0001m;
            int i = 0;
            while (Math.Abs(expected[0] - @out[0]) > delta && Math.Abs(expected[1] - @out[1]) > delta)
            {
                i++;
                @out = p.EvaluateInput(new[] { 1m, 0.1m, 0.7m });
                var errors = new[] { GetError(expected[0],@out[0]), GetError(expected[1] , @out[1]) };
                p.Calculate(expected);
                p.AdjustWeights();
            }
        }

        private static decimal GetError(decimal expected,decimal actual)
        {
            //return expected - actual;
            var diff = actual - expected;
            return 0.5m * diff * diff;
        }
    }
}
