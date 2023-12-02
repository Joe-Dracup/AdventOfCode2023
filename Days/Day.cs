using Helpers;

namespace Days
{
    public abstract class Day(string inputPath) : IDay
    {
        public List<string> Input = new DataAccessor(inputPath).GetInputAsListStrings();

        public abstract string Solve();
    }
}