using KataPencilDurability.Domain.Interfaces;
using System;

namespace KataPencilDurability.Domain
{
    public class Ticonderoga : IPencil, IEraser
    {
        public int PointDurabilityPerSharpening { get; set; }
        public int MaximumNumberOfSharpenings { get; set; }
        public int EraserLetterCapacity { get; set; }
        public int CurrentPointDegradation { get; private set; }
        public int CurrentSharpeningCount { get; private set; }
        public int CurrentEraserCount { get; private set; }

        public Ticonderoga(int pointDurability, int maxNumberOfSharpenings, int eraserLetterCapacity)
        {
            if (pointDurability <= 0)
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

            PointDurabilityPerSharpening = pointDurability;
            MaximumNumberOfSharpenings = maxNumberOfSharpenings;
            EraserLetterCapacity = eraserLetterCapacity;
            CurrentPointDegradation = 0;
            CurrentSharpeningCount = 0;
            CurrentEraserCount = 0; 
        }     

        public void Sharpen()
        {
            if (CurrentSharpeningCount < MaximumNumberOfSharpenings)
            {
                CurrentSharpeningCount += 1;
                CurrentPointDegradation = 0; 
            }
            else
            {
                throw new Exception("Pencil can not be sharpened anymore. Please buy a new pencil."); 
            }
        }

        public char? Write(char inputChar)
        {
            if (char.IsUpper(inputChar))
            {
                if (PointDurabilityPerSharpening - CurrentPointDegradation >= 2)
                {
                    CurrentPointDegradation += 2;
                    return inputChar;
                }
                else if ((PointDurabilityPerSharpening - CurrentPointDegradation == 1))
                {
                    //Requirements don't say what to do with a capital letter with only 1 point durability left. 
                    //Because the pencil can still write a lower case, this logic will return nothing and will not increase the point.
                    return null; 
                }
                else
                {
                    return ' '; 
                }
            }
            else if (char.IsLower(inputChar) && CurrentPointDegradation < PointDurabilityPerSharpening)
            {
                CurrentPointDegradation += 1; 
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
