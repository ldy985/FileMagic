using BenchmarkDotNet.Running;

namespace ldy985.FileMagic.Benchmarks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            BenchmarkRunner.Run<StreamReading>();
        }
    }
}