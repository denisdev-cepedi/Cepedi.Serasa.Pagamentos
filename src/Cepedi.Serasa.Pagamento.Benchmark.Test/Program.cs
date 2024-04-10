// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using Cepedi.Serasa.Pagamento.Benchmark.Test;
using Cepedi.Serasa.Pagamento.Benchmark.Test.Helpers;
using Cepedi.Serasa.Pagamento.Benchmark.Tests;

//var summary = BenchmarkRunner.Run<StringConcatenationVsStringBuilderBenchmark>();
//var summary = BenchmarkRunner.Run<IterationBenchmark>();
//var summary = BenchmarkRunner.Run<ArrayCopyBenchmark>();
var summary = BenchmarkRunner.Run<DapperVsEfCoreBenchmark>();
