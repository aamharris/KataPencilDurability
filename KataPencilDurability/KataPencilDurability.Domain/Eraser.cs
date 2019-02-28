using System;
using System.Text.RegularExpressions;

namespace KataPencilDurability.Domain
{
    public class Eraser
    {
        public int MaxEraserDegradation { get; private set; }
        public int EraserDegradationRemaining { get; private set; }
        public Paper Paper { get; set; }

        public Eraser(Paper paper, int maxEraserDegradation)
        {
            Paper = paper;
            MaxEraserDegradation = maxEraserDegradation;
            EraserDegradationRemaining = MaxEraserDegradation; 
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
                int startSpacesIndex = textToErase.Length - EraserDegradationRemaining;
                erasedText = textToErase.Remove(startSpacesIndex, EraserDegradationRemaining).Insert(startSpacesIndex, new string(' ', EraserDegradationRemaining));
            }
            else
            {
                erasedText = new string(' ', textToErase.Length);
            }

            Paper.LastEditedTextIndex = Regex.Match(Paper.Text, $"{textToErase}(?!.*{textToErase})").Index;
            Paper.Text = Regex.Replace(Paper.Text, $"{textToErase}(?!.*{textToErase})", erasedText);
            EraserDegradationRemaining -= erasedText.Length;
        }
    }
}
