using System;
using System.Text.RegularExpressions;

namespace KataPencilDurability.Domain
{
    public class Pencil
    {
        public Pencil(PencilProperties properties)
        {
            Paper = properties.Paper;

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

        public void Write(string inputText)
        {
            foreach (var letter in inputText)
            {
                if (char.IsUpper(letter) && PointDegradationRemaining >= 2)
                {
                    PointDegradationRemaining -= 2;
                    Paper.Text += letter; 
                }
                else if (char.IsLower(letter) && PointDegradationRemaining >= 1)
                {
                    PointDegradationRemaining -= 1;
                    Paper.Text += letter;
                }
                else
                {
                    Paper.Text += " "; 
                }
            }
        }

        public void Erase(string textToErase)
        {
            if (EraserDegradationRemaining > 0)
            {
                string erasedText;
                if (EraserDegradationRemaining < textToErase.Length)
                {
                    int eraseStartIndex = textToErase.Length - EraserDegradationRemaining;
                    erasedText = textToErase.Remove(eraseStartIndex, EraserDegradationRemaining).Insert(eraseStartIndex, new string(' ', EraserDegradationRemaining)); 
                }
                else
                {
                    erasedText = new string(' ', textToErase.Length); 
                }
                
                Paper.Text = Regex.Replace(Paper.Text, $"{textToErase}(?!.*{textToErase})", erasedText);
                EraserDegradationRemaining -= erasedText.Length;
            }
            else
            {
                throw new Exception("Eraser has been used up. Please buy a new pencil or eraser.");
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
                throw new Exception("Pencil can not be sharpened anymore. Please buy a new pencil.");
            }
        }
    }
}
