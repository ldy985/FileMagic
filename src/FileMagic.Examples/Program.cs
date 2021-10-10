namespace ldy985.FileMagic.Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Example1_DependencyInjection.Start();
            Example2_TryingSpecificRules.Start();
            Example3_QuickPatternMatch.Start();
            Example4_ParsingHandlers.Start();
            Example5_FileType.Start();
        }

        public static string BasePath(uint id)
        {
            return $"../../resources/test{id}.";
        }
    }
}