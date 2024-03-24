using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using LD;

namespace PerformanceTest;
 
[RPlotExporter]
[GcServer]
public class BigDouble
{
    [Benchmark]
    public void ManyExponent()
    {
        for (int i = 0; i < 10000; i++)
        {
            BreakInfinity.BigDouble.Parse("1e1000");
        }
    } 
    [Benchmark]
    public void ManyNumber()
    {
        for (int i = 0; i < 10000; i++)
        {
            BreakInfinity.BigDouble.Parse("10000000000000000000000000000000000000000000000000000000000");
        }
    }
}