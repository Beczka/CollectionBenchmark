using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading;
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
    [Config(typeof(ForceGcCollectionsConfig))]
    public class Test
    {
        [Params(100, 1000, 10000, 100000)]
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
        public void LinkedList()
        {
            var collection = new LinkedList<DummyModel>();

            for (int i = 0; i < Size; i++)
            {
                collection.AddFirst(new DummyModel());
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


        [Benchmark]
        public void ObservableCollection()
        {
            var collection = new ObservableCollection<DummyModel>();

            for (int i = 0; i < Size; i++)
            {
                collection.Add(new DummyModel());
            }
        }

        [Benchmark]
        public void ImmutableDictionary()
        {
            var builder = System.Collections.Immutable.ImmutableDictionary.Create<int, DummyModel>().ToBuilder();

            for (int i = 0; i < Size; i++)
            {
                builder.Add(i ,new DummyModel());
            }

            var collection = builder.ToImmutable();
        }

        [Benchmark]
        public void ImmutableDictionaryWithoutBuilder()
        {
            var collection = System.Collections.Immutable.ImmutableDictionary.Create<int, DummyModel>();
            for (int i = 0; i < Size; i++)
            {
                collection.Add(i, new DummyModel());
            }

        }

        [Benchmark]
        public void ImmutableList()
        {
            var builder = System.Collections.Immutable.ImmutableList.Create<DummyModel>().ToBuilder();

            for (int i = 0; i < Size; i++)
            {
                builder.Add(new DummyModel());
            }

            var collection = builder.ToImmutable();
        }


        [Benchmark]
        public void ImmutableListWithoutBuilder()
        {
            var collection = System.Collections.Immutable.ImmutableList.Create<DummyModel>();
            for (int i = 0; i < Size; i++)
            {
                collection.Add(new DummyModel());
            }

        }

        //[Benchmark]
        //public void ImmutableInterlocked()
        //{
        //    var collection = System.Collections.Immutable.ImmutableInterlocked..Create<DummyModel>();

        //    for (int i = 0; i < Size; i++)
        //    {
        //        collection.Add(new DummyModel());
        //    }
        //}
    }

    public class ForceGcCollectionsConfig : ManualConfig
    {
        public ForceGcCollectionsConfig()
        {
            Add(Job.Default
                .With(new GcMode(){
                    Force = true
                }));
        }
    }
}
