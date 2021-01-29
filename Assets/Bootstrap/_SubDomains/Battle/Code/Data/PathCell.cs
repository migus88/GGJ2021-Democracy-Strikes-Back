namespace Bootstrap._SubDomains.Battle.Code.Data
{
    public class PathCell
    {
        public (int, int) Coordinates { get; set; }
        public bool IsCharacter { get; set; }
        public (int, int) Direction { get; set; }
    }
}