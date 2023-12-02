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
            var firstDigit = line.First(c => Int32.TryParse(c.ToString(), out int _));
            var lastDigit = line.Last(c => Int32.TryParse(c.ToString(), out int _));

            var charConcat = string.Concat(firstDigit, lastDigit);

            if (!Int32.TryParse(charConcat, out int lineTotal))
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

            if (!Int32.TryParse(charConcat, out int lineTotal))
            {
                throw new ApplicationException("your total does not parse!");
            }

            return lineTotal;
        }

        private static int GetValueAsInt(string key)
        {
            if (Int32.TryParse(key, out int value))
            {
                return value;
            }
            else
            {
                switch (key)
                {
                    case "one":
                        return 1;
                    case "two":
                        return 2;
                    case "three":
                        return 3;
                    case "four":
                        return 4;
                    case "five":
                        return 5;
                    case "six":
                        return 6;
                    case "seven":
                        return 7;
                    case "eight":
                        return 8;
                    case "nine":
                        return 9;
                }
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
