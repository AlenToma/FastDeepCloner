using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using System;

namespace BNGTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);
            config.Add(Job.ShortRun.With(InProcessEmitToolchain.Instance));
            var summary = BenchmarkRunner.Run<FastDeepCloner_tests>(config);

            //Console.WriteLine("Hello World!");
        }
    }
}
