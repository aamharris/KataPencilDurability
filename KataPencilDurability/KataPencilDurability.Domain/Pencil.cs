using KataPencilDurability.Domain.Constants;
using KataPencilDurability.Domain.Enums;
using System;

namespace KataPencilDurability.Domain
{
    public class Pencil
    {
        public Pencil(PencilProperties properties)
        {
            Paper = properties.Paper;
            Eraser = properties.Eraser;

            ValidatePencilInitialize(properties);

            MaxPointDegradation = properties.PointDegradation;
            PointDegradationRemaining = MaxPointDegradation;

            MaxNumberOfSharpenings = properties.MaxNumberOfSharpenings;
            SharpeningsRemaining = MaxNumberOfSharpenings;
        }

        public Paper Paper { get; set; }
        public Eraser Eraser { get; private set; }
        public int MaxPointDegradation { get; private set; }
        public int PointDegradationRemaining { get; private set; }
        public int MaxNumberOfSharpenings { get; private set; }
        public int SharpeningsRemaining { get; private set; }

        public void Write(string inputText, WritingMode writingMode = WritingMode.Add)
        {            
            ValidatePencilWrite(writingMode);

            foreach (var letter in inputText)
            {
                char outputChar = GetOutputCharacter(letter);
                DeductPointDegradation(outputChar);
                AddTextToPaper(writingMode, outputChar);
            }

            if (writingMode == WritingMode.Edit)
            {
                ResetEditIndex();
            }
           
        }

        public void Sharpen()
        {
            if (SharpeningsRemaining > 0)
            {
                SharpeningsRemaining -= 1;
                PointDegradationRemaining = MaxPointDegradation;
            }
            else
            {
                throw new Exception(ExceptionMessages.PencilOutOfSharpening);
            }
        }

        private void ValidatePencilInitialize(PencilProperties properties)
        {
            if (properties.PointDegradation <= 0)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessages.PointDegradationNotGreaterThanZero);
            }

            if (properties.MaxNumberOfSharpenings < 0)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessages.MaxSharpeningNotZeroOrGreater);
            }
        }

        private void ValidatePencilWrite(WritingMode writingMode)
        {
            if (writingMode == WritingMode.Edit && Paper.LastEditedTextIndex == -1)
            {
                throw new Exception(ExceptionMessages.NoPreviousErasedTextToEdit);
            }

            if (Paper == null)
            {
                throw new Exception(ExceptionMessages.NoPaperFound);
            }
        }

        private char GetOutputCharacter(char letter)
        {
            char outputChar = ' ';
            if ((char.IsUpper(letter) && PointDegradationRemaining >= 2) || (char.IsLower(letter) && PointDegradationRemaining >= 1))
            {
                outputChar = letter;
            }

            return outputChar;
        }

        private void DeductPointDegradation(char letter)
        {
            if (char.IsUpper(letter))
            {
                PointDegradationRemaining -= 2;
            }

            if (char.IsLower(letter))
            {
                PointDegradationRemaining -= 1;
            }
        }

        private void AddTextToPaper(WritingMode writingMode, char outputChar)
        {
            if (writingMode == WritingMode.Add)
            {
                Paper.Text += outputChar;
            }
            else
            {               
                if (HasCollisionWithExistingText())
                {
                    outputChar = '@';
                }

                Paper.Text = Paper.Text.Remove(Paper.LastEditedTextIndex, 1).Insert(Paper.LastEditedTextIndex, outputChar.ToString());
                Paper.LastEditedTextIndex += 1;
            }
        }

        private void ResetEditIndex()
        {
            Paper.LastEditedTextIndex = -1; 
        }

        private bool HasCollisionWithExistingText()
        {
            char existingChar = Paper.Text[Paper.LastEditedTextIndex];
            return !char.IsWhiteSpace(existingChar); 
        }
    }
}
