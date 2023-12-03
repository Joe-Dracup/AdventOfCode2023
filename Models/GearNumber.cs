namespace Models
{
    public class GearNumber(int number, int gearLineIndex, int gearIndexOnLine)
    {
        public int Number { get; set; } = number;
        public int GearLineIndex { get; set; } = gearLineIndex;
        public int GearIndexOnLine { get; set; } = gearIndexOnLine;

        public override string ToString()
        {
            return $"Number: {Number} GearLineIndex: {GearLineIndex} GearIndexOnLine: {GearIndexOnLine}\n";
        }
    }
}
