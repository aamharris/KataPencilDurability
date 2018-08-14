namespace KataPencilDurability.Domain.Interfaces
{
    public interface IEraser
    {
        int EraserLetterCapacity { get; set; }

        void Erase(string eraseText);
    }
}
