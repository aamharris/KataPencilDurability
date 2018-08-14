namespace KataPencilDurability.Domain.Interfaces
{
    public interface IPencil
    {
        int LetterCapacityPerSharpening { get; set; }

        int MaximumNumberOfSharpenings { get; set; }

        void Write(char character);

        void Sharpen();            
    }
}
