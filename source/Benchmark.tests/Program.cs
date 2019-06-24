using BenchmarkDotNet.Running;
using System;

namespace Benchmarklb
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<FastDeepCloner_tests>(/*new MyConfig()*/);
        }
    }
}
