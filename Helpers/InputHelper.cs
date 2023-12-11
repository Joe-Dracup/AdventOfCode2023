using AdventOfCode2023.Attributes;
using Days;

namespace Helpers
{
    public class InputHelper
    {
        public static string GetDayResponse(string input)
        {
            string outPut = "";

            try
            {
                var day = DayFactory.CreateDay(input);

                var incompleteAttributes = (IncompleteAttribute[])day.GetType().GetCustomAttributes(typeof(IncompleteAttribute), true);

                if(incompleteAttributes.Length > 0)
                {
                    outPut += "this method is incomplete! \n";
                }

                outPut += day.Solve();
            }
            catch (DayDoesNotExistException)
            {
                throw new ApplicationException($"\"{input}\" is not a valid input!");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            
            return outPut;
        }
    }
}