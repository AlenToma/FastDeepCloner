using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNGTest
{
    public class Config : ManualConfig
    {
        public Config()
        {
            Add(DefaultConfig.Instance.GetExporters().ToArray());
            Add(DefaultConfig.Instance.GetLoggers().ToArray());
            Add(DefaultConfig.Instance.GetColumnProviders().ToArray());

            Add(Job.Dry.With(Runtime.Core).WithId("MycoreJob")); // for core 
            Add(Job.Dry.With(Runtime.Clr).WithId("Mynetframework")); // for fullframework
            Add(Job.ShortRun.With(InProcessEmitToolchain.Instance));
        }
    }
}
