using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
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
    [Config(typeof(DontForceGcCollectionsConfig))]
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

        [Benchmark]
        public void Queue()
        {
            var collection = new Queue<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Enqueue(new DummyModel());
            }
        }


        [Benchmark]
        public void Stack()
        {
            var collection = new Stack<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Push(new DummyModel());
            }
        }
    }

    public class DontForceGcCollectionsConfig : ManualConfig
    {
        public DontForceGcCollectionsConfig()
        {
            Add(Job.Default
                .With(new GcMode(){
                    Force = true
                }));
        }
    }
}
