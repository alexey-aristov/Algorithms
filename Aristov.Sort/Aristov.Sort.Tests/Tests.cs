using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Aristov.Sort.Tests
{
    [TestFixture]
    public class Tests
    {
        private int _n = 10;
        private int _max = 100;
        private readonly ILog _log = new LogConsole();

        [Test]
        public void BubbleTest()
        {
            //Arrange
            var list = new int[_n];
            var r = new Random(1000);
            for (int i = 0; i < _n; i++)
            {
                list[i] = r.Next(_max);
            }

            //Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = list.BubbleSort(_log);
            stopwatch.Stop();

            //Assert
            Assert.That(result, Is.Ordered);
            Console.WriteLine($"Bubble sort result: n:{_n}, max:{_max}, time:{stopwatch.Elapsed}");
        }

        [Test]
        public void InsertionTest()
        {
            //Arrange
            var list = new int[_n];
            var r = new Random(1000);
            for (int i = 0; i < _n; i++)
            {
                list[i] = r.Next(_max);
            }

            //Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = list.InsertionSort(_log);
            stopwatch.Stop();

            //Assert
            Assert.That(result, Is.Ordered);
            Console.WriteLine($"Insertion sort result: n:{_n}, max:{_max}, time:{stopwatch.Elapsed}");
        }

        [Test]
        public void RadixTest()
        {
            //Arrange
            var list = new List<int>();
            var r = new Random(1000);
            for (int i = 0; i < _n; i++)
            {
                list.Add(r.Next(_max));
            }

            //Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = list.RadixSort(_log);
            stopwatch.Stop();

            //Assert
            Assert.That(result, Is.Ordered);
            Console.WriteLine($"Radix sort result: n:{_n}, max:{_max}, time:{stopwatch.Elapsed}");
        }

        [Test]
        public void QuickTest()
        {
            //Arrange
            var list = new int[_n];
            var r = new Random(1000);
            for (int i = 0; i < _n; i++)
            {
                list[i] = r.Next(_max);
            }

            //Act
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = list.QuickSort(_log);
            stopwatch.Stop();

            //Assert
            Assert.That(result, Is.Ordered);
            Console.WriteLine($"Insertion sort result: n:{_n}, max:{_max}, time:{stopwatch.Elapsed}");
        }
    }
}
