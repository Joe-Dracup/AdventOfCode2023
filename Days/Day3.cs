using System.Text.RegularExpressions;

namespace Days
{
    public class Day3 : Day
    {
        public Day3() : base("/Inputs/Day3.txt")
        {

        }

        public override string Solve()
        {
            int partOneSolution = GetSumOfPartNumbers();
            int partTwoSolution = 0;
            return $"Day3 Part One solution is: {partOneSolution} {Environment.NewLine}Day3 Part Two solution is: {partTwoSolution} ";
        }

        private int GetSumOfPartNumbers()
        {
            List<int> partNumbers = [];

            for (int i = 0; i < Input.Count; i++)
            {
                Dictionary<string, int> numbersFromInput = GetNumbersFromInput(Input[i]);

                partNumbers.AddRange(FindPartNumbers(i, numbersFromInput));
            }

            return partNumbers.Sum();
        }

        private List<int> FindPartNumbers(int i, Dictionary<string, int> numbersFromInput)
        {
            List<int> partNumbers = [];

            foreach (var number in numbersFromInput)
            {
                int indexOfNumber = number.Value;
                int startIndex = (indexOfNumber == 0) ? indexOfNumber : indexOfNumber - 1;
                int length = GetLength(i, number.Key, indexOfNumber);

                int aboveIndex = i - 1;

                if (aboveIndex >= 0 && IsPartNumber(aboveIndex, number.Key, startIndex, length, out var abovePartNumber))
                {
                    partNumbers.Add(abovePartNumber);
                }

                if (IsPartNumber(i, number.Key, startIndex, length, out var partNumber))
                {
                    partNumbers.Add(partNumber);
                }

                int belowIndex = i + 1;

                if (belowIndex < Input.Count && IsPartNumber(belowIndex, number.Key, startIndex, length, out var belowPartNumber))
                {
                    partNumbers.Add(belowPartNumber);
                }
            }

            return partNumbers;
        }

        private int GetLength(int i, string number, int indexOfNumber)
        {
            if (indexOfNumber == 0)
            {
                return number.Length + 1;
            }

            if (indexOfNumber + number.Length + 2 > Input[i].Length)
            {
                return Input[i].Length - indexOfNumber + 1;
            }

            return number.Length + 2;
        }

        private bool IsPartNumber(int indexToSearch, string number, int startIndex, int length, out int partNumber)
        {
            partNumber = 0;

            string searchString = Input[indexToSearch].Substring(startIndex, length);

            if (searchString.Replace(".", "").Any(x => !char.IsLetterOrDigit(x)))
            {
                if (int.TryParse(number, out var result))
                {
                    partNumber = result;
                    return true;
                }
            }

            return false;
        }

        private Dictionary<string, int> GetNumbersFromInput(string line)
        {
            Dictionary<string, int> numbersFromInput = [];

            bool cont = true;

            while (cont)
            {
                Match match = Regex.Match(line, @"\d+");

                if (!match.Success)
                {
                    cont = false;
                }
                else
                {
                    numbersFromInput.Add(match.Value, match.Index);
                    var regex = new Regex(Regex.Escape(match.Value));
                    line = regex.Replace(line, new string('.', match.Value.Length), 1);
                }
            }

            return numbersFromInput;
        }
    }
}