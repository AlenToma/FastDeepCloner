``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.829 (1803/April2018Update/Redstone4)
Intel Core i7-3820 CPU 3.60GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=3515670 Hz, Resolution=284.4408 ns, Timer=TSC
.NET Core SDK=2.2.300
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Job=ShortRun  Toolchain=InProcessEmitToolchain  IterationCount=3  
LaunchCount=1  WarmupCount=3  

```
|          Method |      Mean |     Error |    StdDev |
|---------------- |----------:|----------:|----------:|
|       ProxyTest |  5.339 us | 2.2928 us | 0.1257 us |
|           Clone |  8.321 us | 0.3805 us | 0.0209 us |
|    DynamicClone |  1.951 us | 0.0963 us | 0.0053 us |
| CloneCollection | 14.884 us | 2.1868 us | 0.1199 us |
