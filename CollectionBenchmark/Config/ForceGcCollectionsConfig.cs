using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace CollectionBenchmark.Config
{
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