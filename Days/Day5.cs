using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Days
{
    public class Day5 : Day
    {
        List<FarmingMapper> farmingMappers = [];

        public Day5()
            : base("/Inputs/Day5.txt") { }

        public override string Solve()
        {
            var partOneSolution = GetLowestCorrespondingLocationNumber();
            int partTwoSolution = 1;
            return $"Day5 Part One solution is: {partOneSolution} {Environment.NewLine}Day5 Part Two solution is: {partTwoSolution} ";
        }

        private BigInteger GetLowestCorrespondingLocationNumber()
        {
            var seeds = GetSeedsAndRemoveFromList();

            // seeds.ForEach(Console.WriteLine);

            farmingMappers = SetupFarmingMappers();

            farmingMappers.ForEach(Console.WriteLine);

            List<BigInteger> penultimateResults = [];

            // Console.WriteLine();

            var result = DoBits(79, "seed");

            Console.WriteLine("result " + result);

            foreach (var seed in seeds)
            {
                penultimateResults.Add(DoBits(seed, "seed"));
            }

            List<BigInteger> actualResults = [];

            foreach (var penultimateResult in penultimateResults)
            {
                actualResults.Add(DoBits(penultimateResult, "location"));
            }

            return actualResults.Min();
        }

        private BigInteger DoBits(BigInteger seed, string source)
        {
            Console.WriteLine(seed + " " + source);

            var retVal = seed;
            var relevantMapper = farmingMappers.Where(x => x.Source == source);

            if (relevantMapper.Any())
            {
                relevantMapper.First().farmingMapperValues.ForEach(Console.WriteLine);

                var possibleNewSeed = relevantMapper
                    .First()
                    .farmingMapperValues
                    .Where(x => x.ValueIsInSourceRange(seed))
                    .FirstOrDefault();

                var nextSource = relevantMapper.First().Destination;

                var nextSeed =
                    possibleNewSeed != null ? possibleNewSeed.GetMappedValue(seed) : seed;

                Console.WriteLine();

                retVal = DoBits(nextSeed, nextSource);
            }

            return retVal;
        }

        private List<BigInteger> GetSeedsAndRemoveFromList()
        {
            List<BigInteger> seeds = [];
            List<string> seedsAsString = [.. Input[0].Replace("seeds: ", "").Split(' ')];

            foreach (var seedString in seedsAsString)
            {
                if (!BigInteger.TryParse(seedString.Trim(), out var seedInt))
                {
                    throw new Exception("could not parse: " + seedString);
                }

                seeds.Add(seedInt);
            }

            Input.RemoveAt(0);
            return seeds;
        }

        private List<FarmingMapper> SetupFarmingMappers()
        {
            List<FarmingMapper> farmingMappers = [];

            FarmingMapper currentMapper = FarmingMapper.BlankObject;

            foreach (var line in Input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (currentMapper != FarmingMapper.BlankObject)
                    {
                        farmingMappers.Add(currentMapper);
                    }

                    continue;
                }

                if (line.Contains("map"))
                {
                    var tempLine = line.Replace(" map:", "");
                    var itemKey = tempLine.Split("-to-")[0];
                    var mappingKey = tempLine.Split("-to-")[1];

                    currentMapper = new FarmingMapper(itemKey, mappingKey);

                    continue;
                }

                var valuesInLine = line.Split(" ");

                if (!BigInteger.TryParse(valuesInLine[0], out var destinationRangeStart))
                {
                    throw new Exception($"Error parsing {valuesInLine[0]} in {line}");
                }

                if (!BigInteger.TryParse(valuesInLine[1], out var sourceRangeStart))
                {
                    throw new Exception($"Error parsing {valuesInLine[1]} in {line}");
                }

                if (!BigInteger.TryParse(valuesInLine[2], out var range))
                {
                    throw new Exception($"Error parsing {valuesInLine[2]} in {line}");
                }

                if (currentMapper != FarmingMapper.BlankObject)
                {
                    FarmingMapperValue fmv = new(destinationRangeStart, sourceRangeStart, range);
                    currentMapper.farmingMapperValues.Add(fmv);
                }
            }

            return farmingMappers;
        }
    }

    public class FarmingMapper
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public List<FarmingMapperValue> farmingMapperValues = [];

        public FarmingMapper(string source, string destination)
        {
            Source = source;
            Destination = destination;
        }

        public override string ToString()
        {
            string x = "";

            farmingMapperValues.ForEach(y => x += y.ToString());

            return $"{Source}-to-{Destination} \n{x}";
        }

        public static FarmingMapper BlankObject = new FarmingMapper("", "");
    }

    public class FarmingMapperValue
    {
        public BigInteger DestinationRangeStart { get; set; }
        public BigInteger DestinationRangeEnd
        {
            get { return DestinationRangeStart + Range; }
        }
        public BigInteger SourceRangeStart { get; set; }
        public BigInteger SourceRangeEnd
        {
            get { return SourceRangeStart + Range; }
        }
        public BigInteger Range { get; set; }

        public FarmingMapperValue(BigInteger destinationRangeStart, BigInteger sourceRangeStart, BigInteger range)
        {
            DestinationRangeStart = destinationRangeStart;
            SourceRangeStart = sourceRangeStart;
            Range = range;
        }

        public bool ValueIsInSourceRange(BigInteger value)
        {
            return SourceRangeStart < value && SourceRangeEnd > value;
        }

        public BigInteger GetMappedValue(BigInteger value)
        {
            if (!ValueIsInSourceRange(value))
            {
                throw new Exception("value is out of range");
            }

            return DestinationRangeEnd - (SourceRangeEnd - value);
        }

        public override string ToString()
        {
            return $"{DestinationRangeStart}-{DestinationRangeEnd} {SourceRangeStart}-{SourceRangeEnd}\n";
        }
    }
}
