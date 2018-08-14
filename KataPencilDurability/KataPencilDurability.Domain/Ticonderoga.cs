using KataPencilDurability.Domain.Interfaces;
using System;

namespace KataPencilDurability.Domain
{
    public class Ticonderoga : IPencil, IEraser
    {
        public int LetterCapacityPerSharpening { get; set; }
        public int MaximumNumberOfSharpenings { get; set; }
        public int EraserLetterCapacity { get; set; }

        public Ticonderoga(int letterCapacity, int maxNumberOfSharpenings, int eraserLetterCapacity)
        {
            this.LetterCapacityPerSharpening = letterCapacity;
            this.MaximumNumberOfSharpenings = maxNumberOfSharpenings; 
            this.EraserLetterCapacity = eraserLetterCapacity; 
        }     

        public void Sharpen()
        {
            throw new NotImplementedException();
        }

        public void Write(char character)
        {
            throw new NotImplementedException();
        }

        public void Erase(string eraseText)
        {
            throw new NotImplementedException();
        }
    }
}
