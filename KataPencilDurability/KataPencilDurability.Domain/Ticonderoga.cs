using KataPencilDurability.Domain.Interfaces;
using System;

namespace KataPencilDurability.Domain
{
    public class Ticonderoga : IPencil, IEraser
    {
        public int LetterCapacityPerSharpening { get; set; }
        public int MaximumNumberOfSharpenings { get; set; }
        public int EraserLetterCapacity { get; set; }
        public int CurrentLetterCount { get; private set; }
        public int CurrentSharpeningCount { get; private set; }
        public int CurrentEraserCount { get; private set; }

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

            LetterCapacityPerSharpening = letterCapacity;
            MaximumNumberOfSharpenings = maxNumberOfSharpenings;
            EraserLetterCapacity = eraserLetterCapacity;
            CurrentLetterCount = 0;
            CurrentSharpeningCount = 0;
            CurrentEraserCount = 0; 
        }     

        public void Sharpen()
        {
            if (CurrentSharpeningCount < MaximumNumberOfSharpenings)
            {
                CurrentSharpeningCount += 1;
                CurrentLetterCount = 0; 
            }
            else
            {
                throw new Exception("Pencil can not be sharpened anymore. Please buy a new pencil."); 
            }
        }

        public char Write(char inputChar)
        {
            if (inputChar != ' ' && CurrentLetterCount < LetterCapacityPerSharpening)
            {
                CurrentLetterCount += 1; 
                return inputChar; 
            }
            else
            {
                return ' '; 
            }
        }

        public void Erase(string eraseText)
        {
            throw new NotImplementedException();
        }
    }
}
