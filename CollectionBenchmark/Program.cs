using System;
using System.Collections.Immutable;
using System.Threading;
using BenchmarkDotNet.Running;

namespace CollectionBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<AddBenchmark>();
            var summary = BenchmarkRunner.Run<LockBenchmark>();
            Console.ReadLine();
        }
    }
}
