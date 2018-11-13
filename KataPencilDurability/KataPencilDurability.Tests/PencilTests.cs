﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataPencilDurability.Domain;
using System;

namespace KataPencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {        
        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenWritingText_AddsTextToExisitingPaperText()
        {
            var paper = new Paper("She sells sea shells"); 
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 50 });

            pencil.Write(" down by the sea shore");           

            Assert.AreEqual("She sells sea shells down by the sea shore", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenLettersMatchDegradationCalc_HasDegradationOfZero()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 4 });

            pencil.Write("text");

            Assert.AreEqual("text", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 0);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenPointDegradationAtZero_LeavesSpacesOnly()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 4 });

            pencil.Write("Text");

            Assert.AreEqual("Tex ", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 0);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Pencil_WhenWritingSpaces_DoesNotAffectDegradation()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10 });

            pencil.Write("a b c");

            Assert.AreEqual("a b c", paper.Text);
            Assert.IsTrue(pencil.PointDegradationRemaining == 7);
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void Pencil_WhenMaxSharpeningsReached_Throws()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 1 }); 
            pencil.Sharpen(); //first time to get the count to 0
            Assert.ThrowsException<Exception>(() => pencil.Sharpen(), "Pencil can not be sharpened anymore. Please buy a new pencil.");
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void Pencil_WhenSharpened_RestoresPointDurability()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3 });
            pencil.Write("some text"); 
            pencil.Sharpen();
            Assert.AreEqual(10, pencil.PointDegradationRemaining);
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void Pencil_WhenSharpened_DeductsSharpeningsRemaining()
        {
            var paper = new Paper();
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3 });
            pencil.Write("some text");
            pencil.Sharpen();
            Assert.AreEqual(2, pencil.SharpeningsRemaining);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void PaperText_WhenErased_RemovesLastInstanceOfWord()
        {
            var paper = new Paper("How much wood would a woodchuck chuck if a woodchuck could chuck wood?");
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, MaxEraserDegradation = 20 });
            pencil.Erase("chuck"); 
            Assert.AreEqual("How much wood would a woodchuck chuck if a woodchuck could       wood?", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void PaperText_WhenErased_RemovesLastInstanceOfCombinedWord()
        {
            var paper = new Paper("How much wood would a woodchuck chuck if a woodchuck could       wood?");
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, MaxEraserDegradation = 20 });
            pencil.Erase("chuck");
            Assert.AreEqual("How much wood would a woodchuck chuck if a wood      could       wood?", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void PaperText_WhenOutOfDegradation_RemovesPartialWord()
        {
            var paper = new Paper("Buffalo Bill");
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, MaxEraserDegradation = 3 });
            pencil.Erase("Bill");
            Assert.AreEqual("Buffalo B   ", paper.Text);
        }

        [TestMethod, TestCategory("Pencil Erase")]
        public void Pencil_WithNoEraserLeft_Throws()
        {
            var paper = new Paper("Buffalo Bill");
            var pencil = new Pencil(new PencilProperties() { Paper = paper, PointDegradation = 10, MaxNumberOfSharpenings = 3, MaxEraserDegradation = 4 });
            pencil.Erase("Bill");
            Assert.ThrowsException<Exception>(() => pencil.Erase("Buffalo"), "Eraser has been used up. Please buy a new pencil or eraser.");
        }
    }
}
