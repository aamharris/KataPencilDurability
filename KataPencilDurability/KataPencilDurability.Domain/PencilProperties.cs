namespace KataPencilDurability.Domain
{
    public class PencilProperties
    {
        public int PointDegradation { get; set; }
        public int MaxNumberOfSharpenings { get; set; }
        public int MaxEraserDegradation { get; set; }
        public Paper Paper { get; set; }
    }
}
