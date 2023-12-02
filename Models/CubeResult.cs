namespace Models
{
    public class CubeResult
    {
        public string CubeType { get; set; }
        public int NumberOfCubes { get; set; }

        public CubeResult(string cubeType, int numberOfCubes)
        {
            CubeType = cubeType;
            NumberOfCubes = numberOfCubes;
        }

        public override string ToString()
        {
            return $"CubeType: {CubeType} Number: {NumberOfCubes}";
        }
    }
}