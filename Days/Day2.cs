using Models;

namespace Days
{
    public class Day2 : Day
    {
        readonly string END_OF_GAMEID_NOTIFIER = ":";

        readonly List<string> JUNKCHARS =
        [
            ": ",
            ";",
            ","
        ];

        public Day2() : base("/Inputs/Day2.txt")
        {

        }

        public override string Solve()
        {
            var partOneSolution = GetSumOfIdsOfImpossibleGames(12, 13, 14);
            var partTwoSolution = GetSumOfPowerOfLowestNumberOfCubes();

            return $"Day2 Part One solution is: {partOneSolution} {Environment.NewLine}Day2 Part Two solution is: {partTwoSolution} ";
        }

        private int GetSumOfPowerOfLowestNumberOfCubes()
        {
            List<int> powerOfLines = [];

            foreach (var line in Input)
            {
                List<CubeResult> lineResults = GetTotalLineResults(line);

                var maxRed = lineResults.Where(x => x.CubeType == "red").Max(x=>x.NumberOfCubes);
                var maxGreen = lineResults.Where(x => x.CubeType == "green").Max(x=>x.NumberOfCubes);
                var maxBlue = lineResults.Where(x => x.CubeType == "blue").Max(x=>x.NumberOfCubes);

                powerOfLines.Add(maxRed * maxGreen * maxBlue);
            }

            return powerOfLines.Sum();
        }

        private int GetSumOfIdsOfImpossibleGames(int numRed, int numGreen, int numBlue)
        {
            List<int> possibleIds = [];

            foreach (var line in Input)
            {
                if (LineIsPossible(line, numRed, numGreen, numBlue, out var id))
                {
                    possibleIds.Add(id);
                }
            }

            return possibleIds.Sum();
        }

        private bool LineIsPossible(string line, int numRed, int numGreen, int numBlue, out int id)
        {
            id = GetLineId(line);

            List<CubeResult> lineResults = GetTotalLineResults(line);

            if (lineResults.Any(x =>
                (x.CubeType == "red" && x.NumberOfCubes > numRed) ||
                (x.CubeType == "green" && x.NumberOfCubes > numGreen) ||
                (x.CubeType == "blue" && x.NumberOfCubes > numBlue)
                ))
            {
                return false;
            }

            return true;
        }

        private List<CubeResult> GetTotalLineResults(string line)
        {
            var alteredLine = line.Remove(0, line.IndexOf(END_OF_GAMEID_NOTIFIER));

            JUNKCHARS.ForEach(x => alteredLine = alteredLine.Replace(x, ""));

            var splitStrings = alteredLine.Split(' ').ToList();

            List<CubeResult> cubeResults = [];

            for (var i = 0; i < splitStrings.Count / 2; i++)
            {
                var numAsString = splitStrings[i * 2];

                if (int.TryParse(numAsString, out var number))
                {
                    var cubeResult = new CubeResult(splitStrings[i * 2 + 1], number);
                    cubeResults.Add(cubeResult);
                }
                else
                {
                    throw new ApplicationException($"could not parse number {numAsString} from line {line}");
                }
            }

            return cubeResults;
        }

        private int GetLineId(string line)
        {
            string stringId = line[..line.IndexOf(END_OF_GAMEID_NOTIFIER)].Replace("Game ", "");

            if (int.TryParse(stringId, out var id))
            {
                return id;
            }

            throw new ApplicationException($"could not parse the id: {stringId} of line {line}");
        }
    }
}