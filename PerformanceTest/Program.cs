// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using PerformanceTest;
 
var res1 = BenchmarkRunner.Run(typeof(FastBigDouble).Assembly);
var res2 = BenchmarkRunner.Run(typeof(BigDouble).Assembly);