namespace KataPencilDurability.Domain.Interfaces
{
    public interface IPencil
    {
        int PointDurabilityPerSharpening { get; set; }

        int MaximumNumberOfSharpenings { get; set; }

        char? Write(char inputChar);

        void Sharpen();            
    }
}
