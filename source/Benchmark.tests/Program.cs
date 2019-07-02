using BenchmarkDotNet.Running;

namespace Benchmarklb
{
    class Program
    {
        static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
