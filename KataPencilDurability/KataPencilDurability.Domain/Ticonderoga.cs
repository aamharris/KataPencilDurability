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
            if (letterCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException("Letter capacity must be greater than 0"); 
            }

            if (maxNumberOfSharpenings <= 0)
            {
                throw new ArgumentOutOfRangeException("Maximum sharpenings must be greater than 0");
            }

            if (eraserLetterCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException("Eraser Capacity must be greater than 0");
            }

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
