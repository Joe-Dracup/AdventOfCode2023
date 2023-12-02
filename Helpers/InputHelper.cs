using Days;

namespace Helpers
{
    public class InputHelper
    {
        public string GetDayResponse(string input)
        {
            string outPut;

            switch (input)
            {
                case "1":
                    outPut = new Day1().Solve();
                    break;
                case "2":
                    outPut = new Day2().Solve();
                    break;
                default:
                    throw new ApplicationException($"\"{input}\" is not a valid input!");
            }
            return outPut;
        }
    }
}