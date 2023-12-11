using System.Numerics;

namespace Days
{
    public class Day6 : Day
    {
        public Day6() : base("/Inputs/Day6.txt")
        {
        }

        public override string Solve()
        {
            BigInteger partOneSolution = GetSumOfNumberOfWinnableWaysForRaces();
            BigInteger partTwoSolution = GetNumberOfWinningRacesForConcatStrings();
            return $"Day6 Part One solution is: {partOneSolution} {Environment.NewLine}Day6 Part Two solution is: {partTwoSolution} ";

        }

        private BigInteger GetNumberOfWinningRacesForConcatStrings()
        {
            string sTime = string.Concat(ParseInputBigIntegeroListOfNumbers(Input[0]).Select(x=>x.ToString()));
            string sDistance = string.Concat(ParseInputBigIntegeroListOfNumbers(Input[1]).Select(x=>x.ToString()));
            
            if (!BigInteger.TryParse(sTime, out var time)){
                throw new FormatException($"sTime {sTime} is not a number");
            }
            
            if (!BigInteger.TryParse(sDistance, out var distance)){
                throw new FormatException($"sDistance {sDistance} is not a number");
            }

            return GetNumberOfPossibleWinningStrategies(time, distance);
        }

        private BigInteger GetSumOfNumberOfWinnableWaysForRaces()
        {
            List<BigInteger> times = ParseInputBigIntegeroListOfNumbers(Input[0]).ToList();
            List<BigInteger> distances = ParseInputBigIntegeroListOfNumbers(Input[1]).ToList();

            if (times.Count() != distances.Count)
            {
                throw new InvalidOperationException($"times count: {times.Count} should match distances count: {distances.Count}");
            }

            List<BigInteger> numberOfWinningStrats = [];

            for (var i = 0; i < times.Count; i++)
            {
                numberOfWinningStrats.Add(GetNumberOfPossibleWinningStrategies(times[i], distances[i]));
            }

            return numberOfWinningStrats.Aggregate((x, y) => x* y);
        }

        private static BigInteger GetNumberOfPossibleWinningStrategies(BigInteger time, BigInteger distance)
        {
            var numberOfWinningStrategies = 0;

            for (var i = 0; i < time; i++)
            {
                var distanceMoved = (time - i) * i;
                
                if (distanceMoved > distance)
                    numberOfWinningStrategies++;
            }

            return numberOfWinningStrategies;
        }

        private IEnumerable<BigInteger> ParseInputBigIntegeroListOfNumbers(string input)
        {
            List<string> strings = [.. input.Split(" ")];

            foreach (var possibleNumber in strings)
            {
                if (BigInteger.TryParse(possibleNumber, out var number))
                {
                    yield return number;
                }
            }
        }
    }
}