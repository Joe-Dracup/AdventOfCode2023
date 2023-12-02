using Days;

namespace Helpers
{
    public class DayFactory
    {
        public IDay CreateDay(string input)
        {
            string fullyQualifiedClassName = "Days.Day" + input;

            Type t = Type.GetType(fullyQualifiedClassName);

            if (t == null)
            {
                throw new DayDoesNotExistException($"Type {fullyQualifiedClassName} does not exist");
            }

            try
            {
                return Activator.CreateInstance(t) as IDay;
            }
            catch
            {
                throw new Exception($"Error Creating Instance Of Type {fullyQualifiedClassName}");
            }
        }
    }
}