namespace KataPencilDurability.Domain
{
    public class PencilProperties
    {
        public int PointDegradation { get; set; }
        public int MaxNumberOfSharpenings { get; set; }
        public Paper Paper { get; set; }
        public Eraser Eraser { get; set; }
    }
}
