``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.829 (1803/April2018Update/Redstone4)
Intel Core i7-3820 CPU 3.60GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=3515670 Hz, Resolution=284.4408 ns, Timer=TSC
.NET Core SDK=2.2.300
  [Host] : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT

LaunchCount=1  

```
|          Method |            Job | Runtime |              Toolchain | IterationCount | RunStrategy | UnrollFactor | WarmupCount |      Mean |     Error |    StdDev |
|---------------- |--------------- |-------- |----------------------- |--------------- |------------ |------------- |------------ |----------:|----------:|----------:|
|       ProxyTest |      MycoreJob |    Core |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
|           Clone |      MycoreJob |    Core |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
|    DynamicClone |      MycoreJob |    Core |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
| CloneCollection |      MycoreJob |    Core |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
|       ProxyTest | Mynetframework |     Clr |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
|           Clone | Mynetframework |     Clr |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
|    DynamicClone | Mynetframework |     Clr |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
| CloneCollection | Mynetframework |     Clr |                Default |              1 |   ColdStart |            1 |           1 |        NA |        NA |        NA |
|       ProxyTest |       ShortRun |    Core | InProcessEmitToolchain |              3 |     Default |           16 |           3 |  5.629 us | 0.4164 us | 0.0228 us |
|           Clone |       ShortRun |    Core | InProcessEmitToolchain |              3 |     Default |           16 |           3 |  8.599 us | 2.6951 us | 0.1477 us |
|    DynamicClone |       ShortRun |    Core | InProcessEmitToolchain |              3 |     Default |           16 |           3 |  1.671 us | 0.2310 us | 0.0127 us |
| CloneCollection |       ShortRun |    Core | InProcessEmitToolchain |              3 |     Default |           16 |           3 | 14.997 us | 1.2749 us | 0.0699 us |

Benchmarks with issues:
  FastDeepCloner_tests.ProxyTest: MycoreJob(Runtime=Core, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
  FastDeepCloner_tests.Clone: MycoreJob(Runtime=Core, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
  FastDeepCloner_tests.DynamicClone: MycoreJob(Runtime=Core, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
  FastDeepCloner_tests.CloneCollection: MycoreJob(Runtime=Core, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
  FastDeepCloner_tests.ProxyTest: Mynetframework(Runtime=Clr, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
  FastDeepCloner_tests.Clone: Mynetframework(Runtime=Clr, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
  FastDeepCloner_tests.DynamicClone: Mynetframework(Runtime=Clr, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
  FastDeepCloner_tests.CloneCollection: Mynetframework(Runtime=Clr, IterationCount=1, LaunchCount=1, RunStrategy=ColdStart, UnrollFactor=1, WarmupCount=1)
