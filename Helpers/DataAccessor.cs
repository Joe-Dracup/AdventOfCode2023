namespace Helpers
{
    public class DataAccessor
    {
        private string BASE_PATH = @"C:\Users\I39125\source\repos\AdventOfCode2023\";
        private readonly string _path;

        public DataAccessor(string path)
        {
            _path = BASE_PATH + path;
        }

        public List<string> GetInputAsListStrings()
        {
            if (!File.Exists(_path))
            {
                throw new FileNotFoundException("could not find file with that path");
            }

            var logFile = File.ReadAllLines(_path);
            return new List<string>(logFile);
        }
    }
}
