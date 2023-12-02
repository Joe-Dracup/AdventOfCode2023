using Helpers;

namespace Days
{
    public abstract class Day
    {
        public List<string> Input;

        public Day(string inputPath)
        {
            Input = new DataAccessor(inputPath).GetInputAsListStrings();
        }
    }
}