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
                throw new Exception("Type is null");
            }
            
            return Activator.CreateInstance(t) as IDay;
        }
    }
}