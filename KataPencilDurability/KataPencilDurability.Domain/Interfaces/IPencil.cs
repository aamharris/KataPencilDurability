namespace KataPencilDurability.Domain.Interfaces
{
    public interface IPencil
    {
        int LetterCapacityPerSharpening { get; set; }

        int MaximumNumberOfSharpenings { get; set; }

        char Write(char inputChar);

        void Sharpen();            
    }
}
