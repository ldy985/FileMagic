using BenchmarkDotNet.Running;

namespace ldy985.FileMagic.Matchers.Signature.Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}