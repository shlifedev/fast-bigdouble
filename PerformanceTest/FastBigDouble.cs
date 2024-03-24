using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using LD;

namespace PerformanceTest;
 
[RPlotExporter]
[GcServer]
[Config(typeof(FastAndDirtyConfig))]
public class FastBigDouble
{
    [Benchmark]
    public void ManyExponent()
    {
        for (int i = 0; i < 10000; i++)
        {
            LD.BigDouble a = new LD.BigDouble("1e1000");
        }
    }
    [Benchmark]
    public void ManyAlphabet()
    {
        for (int i = 0; i < 10000; i++)
        {
            LD.BigDouble a = new LD.BigDouble("999.9ZZZ");
        }
    }
    [Benchmark]
    public void ManyNumber()
    {
        for (int i = 0; i < 10000; i++)
        {
            LD.BigDouble a = new LD.BigDouble("10000000000000000000000000000000000000000000000000000000000");
        }
    }
}