using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarklb
{
    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            this.With(ConfigOptions.DisableOptimizationsValidator);
            Add(DefaultConfig.Instance);
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp20)); // .NET Core 2.0

            Add(Job.Default.With(CsProjClassicNetToolchain.Net471)); // NET 4.6.2
        }
    }
}
