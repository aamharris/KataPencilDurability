using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataPencilDurability.Domain;
using System;
using KataPencilDurability.Domain.Enums;
using KataPencilDurability.Domain.Constants;

namespace KataPencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {
        [TestMethod, TestCategory("Pencil Initialize")]
        public void Pencil_WhenInitializedWithNegativePointDegradation_Throws()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Pencil(new PencilProperties() { Paper = new Paper(), PointDegradation = -1, MaxNumberOfSharpenings = 1, Eraser = eraser }),
                ExceptionMessages.PointDegradationNotGreaterThanZero);
        }

        [TestMethod, TestCategory("Pencil Initialize")]
        public void Pencil_WhenInitializedWithNegativeMaxSharpenings_Throws()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Pencil(new PencilProperties() { Paper = new Paper(), PointDegradation = 20, MaxNumberOfSharpenings = -1, Eraser = eraser }),
                ExceptionMessages.MaxSharpeningNotZeroOrGreater);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenWritingText_AddsTextToExisitingPaperText()
        {
            var paper = new Paper("She sells sea shells");
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 50, MaxNumberOfSharpenings = 10, Eraser = eraser });

            pencil.Write(" down by the sea shore");           

            Assert.AreEqual("She sells sea shells down by the sea shore", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenLettersMatchDegradationCalc_HasDegradationOfZero()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 4, MaxNumberOfSharpenings = 1, Eraser = eraser });

            pencil.Write("text");

            Assert.AreEqual("text", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 0);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenPointDegradationAtZero_LeavesSpacesOnly()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 4, MaxNumberOfSharpenings = 1, Eraser = eraser });

            pencil.Write("Text");

            Assert.AreEqual("Tex ", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 0);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenWritingSpaces_DoesNotAffectDegradation()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 1, Eraser = eraser });

            pencil.Write("a b c");

            Assert.AreEqual("a b c", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 7);
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void Pencil_WhenMaxSharpeningsReached_Throws()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 1, Eraser = eraser }); 
            pencil.Sharpen(); //first time to get the count to 0
            Assert.ThrowsException<Exception>(() => pencil.Sharpen(), ExceptionMessages.PencilOutOfSharpening);
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void Pencil_WhenSharpened_RestoresPointDurability()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Write("some text"); 
            pencil.Sharpen();
            Assert.AreEqual(10, pencil.PointDegradationRemaining);
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void Pencil_WhenSharpened_DeductsSharpeningsRemaining()
        {
            var paper = new Paper();
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Write("some text");
            pencil.Sharpen();
            Assert.AreEqual(2, pencil.SharpeningsRemaining);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void PaperText_WhenErased_RemovesLastInstanceOfWord()
        {
            var paper = new Paper("How much wood would a woodchuck chuck if a woodchuck could chuck wood?");
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Eraser.Erase("chuck"); 
            Assert.AreEqual("How much wood would a woodchuck chuck if a woodchuck could       wood?", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void PaperText_WhenErased_RemovesLastInstanceOfCombinedWord()
        {
            var paper = new Paper("How much wood would a woodchuck chuck if a woodchuck could       wood?");
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Eraser.Erase("chuck");
            Assert.AreEqual("How much wood would a woodchuck chuck if a wood      could       wood?", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void PaperText_WhenOutOfDegradation_RemovesPartialWord()
        {
            var paper = new Paper("Buffalo Bill");
            var eraser = new Eraser(paper, 3);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Eraser.Erase("Bill");
            Assert.AreEqual("Buffalo B   ", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void Pencil_WithNoEraserLeft_Throws()
        {
            var paper = new Paper("Buffalo Bill");
            var eraser = new Eraser(paper, 4);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Eraser.Erase("Bill");
            Assert.ThrowsException<Exception>(() => pencil.Eraser.Erase("Buffalo"), "Eraser has been used up. Please buy a new pencil or eraser.");
        }

        [TestMethod, TestCategory("Pencil Edit")]
        public void PaperText_WithEditExactLength_ReplacesLastErasedWithString()
        {
            var paper = new Paper("An apple a day keeps the doctor away");
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });            
            pencil.Eraser.Erase("apple");
            pencil.Write("onion", WritingMode.Edit);
            Assert.AreEqual("An onion a day keeps the doctor away", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Edit")]
        public void PaperText_WithEditOverlappingCurrentText_ShowsCollisionCharacters()
        {
            var paper = new Paper("An apple a day keeps the doctor away");
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Eraser.Erase("apple");
            pencil.Write("artichoke", WritingMode.Edit);
            Assert.AreEqual("An artich@k@ay keeps the doctor away", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Edit")]
        public void Pencil_EditWithNoPreviousErase_Throws()
        {
            var paper = new Paper("An apple a day keeps the doctor away");
            var eraser = new Eraser(paper, 20);
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Write("artichoke", WritingMode.Edit);
            Assert.ThrowsException<Exception>(() => pencil.Write("artichoke", WritingMode.Edit), ExceptionMessages.NoPreviousErasedTextToEdit);
        }

        [TestMethod, TestCategory("Pencil Edit")]
        public void Pencil_ConsecutiveEditsWithoutErase_Throws()
        {
            var paper = new Paper("An apple a day keeps the doctor away");
            var eraser = new Eraser(paper, 20); 
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, Eraser = eraser });
            pencil.Eraser.Erase("apple");
            pencil.Write("onion", WritingMode.Edit);            
            Assert.ThrowsException<Exception>(() => pencil.Write("artichoke", WritingMode.Edit), ExceptionMessages.NoPreviousErasedTextToEdit);
        }
    }
}
