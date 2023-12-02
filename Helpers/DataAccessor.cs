namespace Helpers
{
    public class DataAccessor
    {
        private readonly string _path;

        public DataAccessor(string path)
        {
            string BASE_PATH = Directory.GetCurrentDirectory();
            
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
