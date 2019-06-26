using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using CollectionBenchmark.Config;
using CollectionBenchmark.Models;

namespace CollectionBenchmark
{

    public class DummyModelLock
    {
        public int Counter { get; set; }

        public DummyModelLock()
        {
            Counter = 0;
        }
    }

    [MemoryDiagnoser]
    [Config(typeof(ForceGcCollectionsConfig))]
    public class LockBenchmark
    {
        [Params(100)]
        public int Size { get; set; }

        internal struct SimpleSpinLock
        {
            private int _mResourceInUse;

            public void Enter()
            {
                while (true)
                {
                    // Always set resource to in-use
                    // When this thread changes it from not in-use, return
                    if (Interlocked.Exchange(ref _mResourceInUse, 1) == 0) return;
                    // Black magic goes here...
                }
            }
            public void Leave()
            {
                // Set resource to not in-use
                Volatile.Write(ref _mResourceInUse, 0);
            }
        }

        [Benchmark]
        public void SimpleSpinLock_Benchmark()
        {
            var m_sl = new SimpleSpinLock();
            var model = new DummyModelLock();

            void Action()
            {
                m_sl.Enter();
                for (int i = 0; i < Size; i++)
                {
                    try
                    {
                        model.Counter += (i % 10);
                    }
                    finally
                    {

                    }
                }
                m_sl.Leave();
            }

            Parallel.Invoke(Action, Action, Action);
        }


        [Benchmark]
        public void SpinLock_Benchmark()
        {
            var sl = new SpinLock();
            var model = new DummyModelLock();


            void Action()
            {
                for (int i = 0; i < Size; i++)
                {
                    var gotLock = false;
                    try
                    {
                        sl.Enter(ref gotLock);
                        model.Counter += (i % 10);
                    }
                    finally
                    {
                        // Only give up the lock if you actually acquired it
                        if (gotLock) sl.Exit();
                    }
                }
            }

            Parallel.Invoke( Action, Action, Action);
        }

        [Benchmark]
        public void Lock_Benchmark()
        {
            object balanceLock = new object();
            var model = new DummyModelLock();

            void Action()
            {
                lock (balanceLock)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        try
                        {
                            model.Counter += (i % 10);
                        }
                        finally
                        {
                            
                        }
                    }
                }
            }

            Parallel.Invoke(Action, Action, Action);
        }
    }
}
