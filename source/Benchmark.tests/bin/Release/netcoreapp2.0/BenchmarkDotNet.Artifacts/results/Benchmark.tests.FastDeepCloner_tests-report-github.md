``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.765 (1803/April2018Update/Redstone4)
Intel Core i7-3820 CPU 3.60GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=3515670 Hz, Resolution=284.4408 ns, Timer=TSC
.NET Core SDK=2.2.300
  [Host] : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT  [AttachedDebugger]


```
|          Method |    Job | Runtime | Mean | Error | Ratio | RatioSD | Rank |
|---------------- |------- |-------- |-----:|------:|------:|--------:|-----:|
|       ProxyTest |    Clr |     Clr |   NA |    NA |     ? |       ? |    ? |
|       ProxyTest |   Core |    Core |   NA |    NA |     ? |       ? |    ? |
|       ProxyTest | CoreRT |  CoreRT |   NA |    NA |     ? |       ? |    ? |
|                 |        |         |      |       |       |         |      |
|           Clone |    Clr |     Clr |   NA |    NA |     ? |       ? |    ? |
|           Clone |   Core |    Core |   NA |    NA |     ? |       ? |    ? |
|           Clone | CoreRT |  CoreRT |   NA |    NA |     ? |       ? |    ? |
|                 |        |         |      |       |       |         |      |
|    DynamicClone |    Clr |     Clr |   NA |    NA |     ? |       ? |    ? |
|    DynamicClone |   Core |    Core |   NA |    NA |     ? |       ? |    ? |
|    DynamicClone | CoreRT |  CoreRT |   NA |    NA |     ? |       ? |    ? |
|                 |        |         |      |       |       |         |      |
| CloneCollection |    Clr |     Clr |   NA |    NA |     ? |       ? |    ? |
| CloneCollection |   Core |    Core |   NA |    NA |     ? |       ? |    ? |
| CloneCollection | CoreRT |  CoreRT |   NA |    NA |     ? |       ? |    ? |

Benchmarks with issues:
  FastDeepCloner_tests.ProxyTest: Clr(Runtime=Clr)
  FastDeepCloner_tests.ProxyTest: Core(Runtime=Core)
  FastDeepCloner_tests.ProxyTest: CoreRT(Runtime=CoreRT)
  FastDeepCloner_tests.Clone: Clr(Runtime=Clr)
  FastDeepCloner_tests.Clone: Core(Runtime=Core)
  FastDeepCloner_tests.Clone: CoreRT(Runtime=CoreRT)
  FastDeepCloner_tests.DynamicClone: Clr(Runtime=Clr)
  FastDeepCloner_tests.DynamicClone: Core(Runtime=Core)
  FastDeepCloner_tests.DynamicClone: CoreRT(Runtime=CoreRT)
  FastDeepCloner_tests.CloneCollection: Clr(Runtime=Clr)
  FastDeepCloner_tests.CloneCollection: Core(Runtime=Core)
  FastDeepCloner_tests.CloneCollection: CoreRT(Runtime=CoreRT)
