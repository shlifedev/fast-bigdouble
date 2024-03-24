using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using LD;

namespace PerformanceTest;
 
[RPlotExporter]
[GcServer]
public class FastBigDouble
{
    [Benchmark]
    public void ManyExponent()
    {
        for (int i = 0; i < 10000; i++)
        {
            LD.FastBigDouble a = new LD.FastBigDouble("1e1000");
        }
    }
    [Benchmark]
    public void ManyAlphabet()
    {
        for (int i = 0; i < 10000; i++)
        {
            LD.FastBigDouble a = new LD.FastBigDouble("999.9ZZZ");
        }
    }
    [Benchmark]
    public void ManyNumber()
    {
        for (int i = 0; i < 10000; i++)
        {
            LD.FastBigDouble a = new LD.FastBigDouble("10000000000000000000000000000000000000000000000000000000000");
        }
    }
}