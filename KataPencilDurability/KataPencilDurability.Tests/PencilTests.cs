using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataPencilDurability.Domain;
using System;

namespace KataPencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {        
        [TestMethod, TestCategory("Pencil Write")]
        public void Write_AddsTextToPaper_WithExistingText()
        {
            var paper = new Paper("She sells sea shells"); 
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 50 });

            pencil.Write(" down by the sea shore");           

            Assert.AreEqual("She sells sea shells down by the sea shore", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Write_HasDegradationOfZero_WhenLettersMatchDegradationCalc()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 4 });

            pencil.Write("text");

            Assert.AreEqual("text", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 0);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Write_LeavesSpaces_AfterDegradationIsAtZero()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 4 });

            pencil.Write("Text");

            Assert.AreEqual("Tex ", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 0);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Write_WhenUsingSpaces_DoesNotAffectDegradation()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10 });

            pencil.Write("a b c");

            Assert.AreEqual("a b c", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 7);
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void SharpenPencil_WhenMaxSharpeningsReached_Throws()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 1 }); 
            pencil.Sharpen(); //first time to get the count to 0
            Assert.ThrowsException<Exception>(() => pencil.Sharpen(), "Pencil can not be sharpened anymore. Please buy a new pencil.");
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void SharpenPencil_RestoresPointDurability_WhenSharpened()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3 });
            pencil.Write("some text"); 
            pencil.Sharpen();
            Assert.AreEqual(10, pencil.PointDegradationRemaining);
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void SharpenPencil_DeductsSharpeningsRemaining_WhenSharpened()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3 });
            pencil.Write("some text");
            pencil.Sharpen();
            Assert.AreEqual(2, pencil.SharpeningsRemaining);
        }
    }
}
