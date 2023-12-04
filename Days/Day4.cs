namespace Days
{
    public class Day4 : Day
    {
        public Day4()
            : base("/Inputs/Day4.txt") { }

        public override string Solve()
        {
            Console.WriteLine();

            int partOneSolution = SumOfPoints();
            int partTwoSolution = GetSumOfReccuringCardPoints();
            return $"Day4 Part One solution is: {partOneSolution} {Environment.NewLine}Day4 Part Two solution is: {partTwoSolution} ";
        }

        private int GetSumOfReccuringCardPoints()
        {
            int totalCards = Input.Count;
            for (int i = 0; i < Input.Count; i++)
            {
                totalCards += GetRecursiveWinningNumbers(i);
            }

            return totalCards;
        }

        private int GetRecursiveWinningNumbers(int index)
        {
            int addedScratchCards = 0;

            List<string> winningNumbers = GetWinningNumbers(Input[index]).ToList();

            for (int i = 1; i < winningNumbers.Count + 1; i++)
            {
                if (index + i < Input.Count)
                {
                    addedScratchCards += 1;
                    addedScratchCards += GetRecursiveWinningNumbers(index + i);
                }
            }

            return addedScratchCards;
        }

        private int SumOfPoints()
        {
            int total = 0;

            foreach (var card in Input)
            {
                var numberOfWinningNumbers = GetWinningNumbers(card).Count();

                // Console.WriteLine("winning numbers count: " + numberOfWinningNumbers);

                if (numberOfWinningNumbers > 0)
                {
                    var points = CalcPoints(numberOfWinningNumbers);

                    // Console.WriteLine("points: " + points);

                    total += points;
                }
            }

            return total;
        }

        public static int CalcPoints(int n)
        {
            int points = 1;

            for (int i = 0; i < n - 1; i++)
            {
                points *= 2;
            }

            return points;
        }

        private static IEnumerable<string> GetWinningNumbers(string card)
        {
            card = RemoveCardLabel(card);

            var splitList = card.Split("|");

            var winningNumbers = splitList[0].Trim().Replace("  ", " ").Split(" ").ToList();

            var myNumbers = splitList[1].Trim().Replace("  ", " ").Split(" ").ToList();

            var myWinningNumbers = myNumbers.Where(winningNumbers.Contains);

            return myWinningNumbers;
        }

        private static string RemoveCardLabel(string card)
        {
            return card[(card.IndexOf(": ") + 1)..];
        }
    }
}
