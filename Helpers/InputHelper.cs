using Days;

namespace Helpers
{
    public class InputHelper
    {
        public string GetDayResponse(string input)
        {
            string outPut;

            try
            {
                var day = new DayFactory().CreateDay(input);
                outPut = day.Solve();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"\"{input}\" is not a valid input!");
            }
            
            return outPut;
        }
    }
}