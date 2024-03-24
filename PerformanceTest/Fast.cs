using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

public class FastAndDirtyConfig : ManualConfig
{
    public FastAndDirtyConfig()
    {
        Add(DefaultConfig.Instance); // *** add default loggers, reporters etc? ***

        Add(Job.Default
                .WithLaunchCount(1)     // benchmark process will be launched only once
                .WithIterationTime(new TimeInterval(100)) // 100ms per iteration
                .WithWarmupCount(3)  // 3 warmup iteration 
        );
        
        
    }
 
}