using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;
using CollectionBenchmark.Config;
using CollectionBenchmark.Models;

namespace CollectionBenchmark
{
    [MemoryDiagnoser]
    [Config(typeof(ForceGcCollectionsConfig))]
    public class AddBenchmark
    {
        [Params(100, 1000)]
        public int Size { get; set; }

        [Benchmark]
        public void Array_RefType()
        {
            var collection = new DummyModel[Size];

            for (int i = 0; i < Size; i++)
            {
                collection[i] = new DummyModel();
            }
        }

        [Benchmark]
        public void Array_ValueType()
        {
            var collection = new int[Size];

            for (int i = 0; i < Size; i++)
            {
                collection[i] = i;
            }
        }

        [Benchmark]
        public void ArrayPool_RefType()
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
        public void ArrayPool_ValueType()
        {
            var myPool = ArrayPool<int>.Shared;
            var collection = myPool.Rent(Size);

            for (int i = 0; i < Size; i++)
            {
                collection[i] = i;
            }

            myPool.Return(collection);
        }

        [Benchmark]
        public void List_RefType()
        {
            var collection = new List<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(new DummyModel());
            }
        }

        [Benchmark]
        public void List_ValueType()
        {
            var collection = new List<int>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(i);
            }
        }

        [Benchmark]
        public void LinkedList_RefType()
        {
            var collection = new LinkedList<DummyModel>();

            for (int i = 0; i < Size; i++)
            {
                collection.AddFirst(new DummyModel());
            }
        }

        [Benchmark]
        public void LinkedList_ValueType()
        {
            var collection = new LinkedList<int>();

            for (int i = 0; i < Size; i++)
            {
                collection.AddFirst(i);
            }
        }

        [Benchmark]
        public void Dictionary_RefType()
        {
            var collection = new Dictionary<int, DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(i, new DummyModel());
            }
        }

        [Benchmark]
        public void Dictionary_ValueType()
        {
            var collection = new Dictionary<int, int>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(i, i);
            }
        }

        [Benchmark]
        public void HashSet_RefType()
        {
            var collection = new HashSet<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(new DummyModel());
            }
        }

        [Benchmark]
        public void HashSet_ValueType()
        {
            var collection = new HashSet<int>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Add(i);
            }
        }

        [Benchmark]
        public void Queue_RefType()
        {
            var collection = new Queue<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Enqueue(new DummyModel());
            }
        }

        [Benchmark]
        public void Queue_ValueType()
        {
            var collection = new Queue<int>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Enqueue(i);
            }
        }

        [Benchmark]
        public void Stack_RefType()
        {
            var collection = new Stack<DummyModel>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Push(new DummyModel());
            }
        }

        [Benchmark]
        public void Stack_ValueType()
        {
            var collection = new Stack<int>(Size);

            for (int i = 0; i < Size; i++)
            {
                collection.Push(i);
            }
        }

        [Benchmark]
        public void ObservableCollection_RefType()
        {
            var collection = new ObservableCollection<DummyModel>();

            for (int i = 0; i < Size; i++)
            {
                collection.Add(new DummyModel());
            }
        }

        [Benchmark]
        public void ObservableCollection_ValueType()
        {
            var collection = new ObservableCollection<int>();

            for (int i = 0; i < Size; i++)
            {
                collection.Add(i);
            }
        }

        //[Benchmark]
        //public void ImmutableDictionary()
        //{
        //    var builder = System.Collections.Immutable.ImmutableDictionary.Create<int, DummyModel>().ToBuilder();

        //    for (int i = 0; i < Size; i++)
        //    {
        //        builder.Add(i ,new DummyModel());
        //    }

        //    var collection = builder.ToImmutable();
        //}

        //[Benchmark]
        //public void ImmutableDictionaryWithoutBuilder()
        //{
        //    var collection = System.Collections.Immutable.ImmutableDictionary.Create<int, DummyModel>();
        //    for (int i = 0; i < Size; i++)
        //    {
        //        collection.Add(i, new DummyModel());
        //    }

        //}

        //[Benchmark]
        //public void ImmutableList()
        //{
        //    var builder = System.Collections.Immutable.ImmutableList.Create<DummyModel>().ToBuilder();

        //    for (int i = 0; i < Size; i++)
        //    {
        //        builder.Add(new DummyModel());
        //    }

        //    var collection = builder.ToImmutable();
        //}


        //[Benchmark]
        //public void ImmutableListWithoutBuilder()
        //{
        //    var collection = System.Collections.Immutable.ImmutableList.Create<DummyModel>();
        //    for (int i = 0; i < Size; i++)
        //    {
        //        collection.Add(new DummyModel());
        //    }

        //}

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
}