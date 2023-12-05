using Days;

namespace Helpers
{
    public class DayFactory
    {
        public static IDay CreateDay(string input)
        {
            string fullyQualifiedClassName = "Days.Day" + input;

            Type t = Type.GetType(fullyQualifiedClassName) ?? throw new DayDoesNotExistException($"Type {fullyQualifiedClassName} does not exist");
            
            try
            {
                return Activator.CreateInstance(t) as IDay
                    ?? throw new Exception();
            }
            catch
            {
                throw new Exception($"Error Creating Instance Of Type {fullyQualifiedClassName}");
            }
        }
    }
}