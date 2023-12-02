using Helpers;

namespace Days
{
    public abstract class Day : IDay
    {
        public List<string> Input;

        public Day(string inputPath)
        {
            Input = new DataAccessor(inputPath).GetInputAsListStrings();
        }

        public abstract string Solve();
    }
}