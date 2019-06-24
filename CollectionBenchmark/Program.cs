using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CollectionBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Test>();
            Console.ReadLine();
        }
    }

    public class DummyModel
    {
    }

    [MemoryDiagnoser]
    public class Test
    {
        [Params(100, 1000, 10000)]
        public int Size { get; set; }


        [Benchmark]
        public void Array()
        {
            var collection = new DummyModel[Size];

            for (int i = 0; i < Size; i++)
            {
                collection[i] = new DummyModel();
            }
        }

        [Benchmark]
        public void ArrayPool()
        {
            var myPool = ArrayPool<DummyModel>.Shared;
            var collection = myPool.Rent(Size);

            for (int i = 0; i < Size; i++)
            {
                collection[i] = new DummyModel();
            }

            myPool.Return(collection);
        }

        [Benchmark]
        public void List()
        {
            var collection = new List<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(new DummyModel());
            }
        }

        [Benchmark]
        public void Dictionary()
        {
            var collection = new Dictionary<int, DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(i, new DummyModel());
            }
        }

        [Benchmark]
        public void HashSet()
        {
            var collection = new HashSet<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(new DummyModel());
            }
        }
    }
}
