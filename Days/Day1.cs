namespace Days
{
    public class Day1 : Day
    {
        private readonly List<string> keyStrings =
        [
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
        ];

        public Day1() : base("/Inputs/Day1.txt")
        {
            
        }

        public override string Solve()
        {
            var partOneSolution = SolvePartOne();
            var partTwoSolution = SolvePartTwo();

            return $"Day1 Part One solution is: {partOneSolution} {Environment.NewLine}Day1 Part Two solution is: {partTwoSolution} ";
        }

        private int SolvePartOne()
        {
            int overallTotal = 0;

            foreach (var line in Input)
            {
                int lineTotal = FindLineTotal(line);

                overallTotal += lineTotal;
            }

            return overallTotal;
        }

        private static int FindLineTotal(string line)
        {
            var firstDigit = line.First(c => int.TryParse(c.ToString(), out int _));
            var lastDigit = line.Last(c => int.TryParse(c.ToString(), out int _));

            var charConcat = string.Concat(firstDigit, lastDigit);

            if (!int.TryParse(charConcat, out int lineTotal))
            {
                throw new ApplicationException("your total does not parse!");
            }

            return lineTotal;
        }

        private int SolvePartTwo()
        {
            int overallTotal = 0;

            foreach (var line in Input)
            {
                int lineTotal = FindLineTotalIncludingKeyStrings(line);

                overallTotal += lineTotal;
            }

            return overallTotal;
        }

        private int FindLineTotalIncludingKeyStrings(string line)
        {
            Dictionary<string, int> minValues = FindMinValues(line);
            Dictionary<string, int> maxValues = FindMaxValues(line);

            if (minValues.Count == 0)
            {
                throw new ApplicationException("found no matching values on line: " + line);
            }

            if (maxValues.Count == 0)
            {
                throw new ApplicationException("found no matching values on line: " + line);
            }

            var firstDigit = GetValueAsInt(minValues.MinBy(x => x.Value).Key);

            var secondDigit = GetValueAsInt(maxValues.MaxBy(x => x.Value).Key);

            var charConcat = string.Concat(firstDigit, secondDigit);

            if (!int.TryParse(charConcat, out int lineTotal))
            {
                throw new ApplicationException("your total does not parse!");
            }

            return lineTotal;
        }

        private static int GetValueAsInt(string key)
        {
            if (int.TryParse(key, out int value))
            {
                return value;
            }

            var valueMap = new Dictionary<string, int>
            {
                {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4},
                {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}
            };

            if (valueMap.TryGetValue(key, out int mappedValue))
            {
                return mappedValue;
            }

            throw new ApplicationException(
                $"you done goofed if u got here brother but here is your key {key} "
            );
        }

        private Dictionary<string, int> FindMinValues(string line)
        {
            var founds = keyStrings
                .Select(sub => new { value = line.IndexOf(sub), key = sub })
                .Where(i => i.value >= 0);

            Dictionary<string, int> dict = [];

            foreach (var found in founds)
            {
                dict.Add(found.key, found.value);
            }

            return dict;
        }

        private Dictionary<string, int> FindMaxValues(string line)
        {
            var founds = keyStrings
                .Select(sub => new { value = line.LastIndexOf(sub), key = sub })
                .Where(i => i.value >= 0);

            Dictionary<string, int> dict = [];

            foreach (var found in founds)
            {
                dict.Add(found.key, found.value);
            }

            return dict;
        }
    }
}
