using Models;

namespace Days
{
    public class Day7 : Day
    {
        public Day7() : base("/Inputs/Test.txt")
        {
        }

        public override string Solve()
        {
            int partOneSolution = MultiplyerOfWinnings();
            int partTwoSolution = 1;
            return $"Day7 Part One solution is: {partOneSolution} {Environment.NewLine}Day7 Part Two solution is: {partTwoSolution} ";
        }

        private int MultiplyerOfWinnings()
        {
            List<CardTurn> turns = GetTurns();

            turns = turns.OrderBy(x => x.Score).ThenByDescending(x=>x.HandAsAlphabetical).ToList();

            int result = 0;
            int order = 1;

            foreach(var turn in turns)
            {
                result += (turn.Bid * order);  
                
                // Console.WriteLine($"bid: {turn.Bid} order: {order} result: {result} score: {turn.Score} handAsNumber {turn.HandAsAlphabetical}");
                
                order++;
            }

            return result;
        }

        private List<CardTurn> GetTurns()
        {
            List<CardTurn> turns = [];

            foreach (var input in Input)
            {
                var split = input.Split(' ');

                if (split[0].Count() != 5)
                {
                    throw new Exception($"Hand count for {input} must be 5");
                }

                if (!int.TryParse(split[1], out var result))
                {
                    throw new Exception($"{split[1]} does not parse to int");
                }

                CardTurn turn = new(split[0], result);
                turns.Add(turn);
            }

            return turns;
        }
    }
}