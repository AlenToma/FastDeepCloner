``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.829 (1803/April2018Update/Redstone4)
Intel Core i7-3820 CPU 3.60GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=3515663 Hz, Resolution=284.4414 ns, Timer=TSC
.NET Core SDK=2.2.300
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT


```
|          Method |      Job | Runtime |              Toolchain | IterationCount | LaunchCount | WarmupCount |      Mean |     Error |    StdDev |
|---------------- |--------- |-------- |----------------------- |--------------- |------------ |------------ |----------:|----------:|----------:|
|       ProxyTest |      Clr |     Clr |                Default |        Default |     Default |     Default |        NA |        NA |        NA |
|           Clone |      Clr |     Clr |                Default |        Default |     Default |     Default |        NA |        NA |        NA |
|    DynamicClone |      Clr |     Clr |                Default |        Default |     Default |     Default |        NA |        NA |        NA |
| CloneCollection |      Clr |     Clr |                Default |        Default |     Default |     Default |        NA |        NA |        NA |
|       ProxyTest | ShortRun |    Core | InProcessEmitToolchain |              3 |           1 |           3 |  6.024 us | 0.5737 us | 0.0314 us |
|           Clone | ShortRun |    Core | InProcessEmitToolchain |              3 |           1 |           3 |  8.885 us | 3.5352 us | 0.1938 us |
|    DynamicClone | ShortRun |    Core | InProcessEmitToolchain |              3 |           1 |           3 |  2.012 us | 1.1436 us | 0.0627 us |
| CloneCollection | ShortRun |    Core | InProcessEmitToolchain |              3 |           1 |           3 | 15.545 us | 4.0534 us | 0.2222 us |

Benchmarks with issues:
  FastDeepCloner_tests.ProxyTest: Clr(Runtime=Clr)
  FastDeepCloner_tests.Clone: Clr(Runtime=Clr)
  FastDeepCloner_tests.DynamicClone: Clr(Runtime=Clr)
  FastDeepCloner_tests.CloneCollection: Clr(Runtime=Clr)
