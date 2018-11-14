using KataPencilDurability.Domain.Enums;
using System;
using System.Text.RegularExpressions;

namespace KataPencilDurability.Domain
{
    public class Pencil
    {
        public Pencil(PencilProperties properties)
        {
            Paper = properties.Paper;

            ValidatePencilInitialize(properties); 

            MaxPointDegradation = properties.PointDegradation;
            PointDegradationRemaining = MaxPointDegradation;

            MaxNumberOfSharpenings = properties.MaxNumberOfSharpenings;
            SharpeningsRemaining = MaxNumberOfSharpenings;

            MaxEraserDegradation = properties.MaxEraserDegradation;
            EraserDegradationRemaining = MaxEraserDegradation;           
        }

        public Paper Paper { get; set; }
        public int MaxPointDegradation { get; private set; }
        public int PointDegradationRemaining { get; private set; }
        public int MaxNumberOfSharpenings { get; private set; }
        public int SharpeningsRemaining { get; private set; }
        public int MaxEraserDegradation { get; private set; }
        public int EraserDegradationRemaining { get; private set; }

        private int eraseStartIndex { get; set; }

        public void Write(string inputText, WritingMode writingMode = WritingMode.Add)
        {
            ValidatePencilWrite(writingMode);

            foreach (var letter in inputText)
            {
                char outputChar = GetOutputCharacter(letter);
                DeductPointDegradation(outputChar);
                AddTextToPaper(writingMode, outputChar);
            }

            eraseStartIndex = -1;
        }      

        public void Erase(string textToErase)
        {
            if (Paper == null)
            {
                throw new Exception("No paper was provided. Can not erase without paper.");
            }

            if (EraserDegradationRemaining == 0)
            {
                throw new Exception("Eraser has been used up. Please buy a new pencil or eraser.");
            }

            string erasedText;
            if (EraserDegradationRemaining < textToErase.Length)
            {
                eraseStartIndex = textToErase.Length - EraserDegradationRemaining;
                erasedText = textToErase.Remove(eraseStartIndex, EraserDegradationRemaining).Insert(eraseStartIndex, new string(' ', EraserDegradationRemaining)); 
            }
            else
            {
                erasedText = new string(' ', textToErase.Length); 
            }
            
            eraseStartIndex = Regex.Match(Paper.Text, $"{textToErase}(?!.*{textToErase})").Index;

            Paper.Text = Regex.Replace(Paper.Text, $"{textToErase}(?!.*{textToErase})", erasedText);
            EraserDegradationRemaining -= erasedText.Length;                                                          
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
                throw new Exception("Pencil can not be sharpened anymore. Please buy a new pencil.");
            }
        }
        
        private void ValidatePencilInitialize(PencilProperties properties)
        {
            if (properties.PointDegradation <= 0)
            {
                throw new ArgumentOutOfRangeException("Point Degradation must be greater than 0");
            }

            if (properties.MaxNumberOfSharpenings < 0)
            {
                throw new ArgumentOutOfRangeException("Maximum sharpenings must be 0 or greater");
            }

            if (properties.MaxEraserDegradation <= 0)
            {
                throw new ArgumentOutOfRangeException("Eraser Degradation must be greater than 0");
            }
        }

        private void ValidatePencilWrite(WritingMode writingMode)
        {
            if (writingMode == WritingMode.Edit && eraseStartIndex == -1)
            {
                throw new Exception("No text has been erased. Cannot Edit Text");
            }

            if (Paper == null)
            {
                throw new Exception("No paper was provided. Can not write without paper.");
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

                Paper.Text = Paper.Text.Remove(eraseStartIndex, 1).Insert(eraseStartIndex, outputChar.ToString());
                eraseStartIndex += 1;
            }
        }

        private bool HasCollisionWithExistingText()
        {
            char existingChar = Paper.Text[eraseStartIndex];
            return !char.IsWhiteSpace(existingChar); 
        }
    }
}
