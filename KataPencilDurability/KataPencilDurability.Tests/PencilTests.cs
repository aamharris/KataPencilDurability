using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataPencilDurability.Domain;

namespace KataPencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {
        [TestMethod, TestCategory("Pencil Creation")]
        public void Constructor_ValidAmounts_SetsPencilProperties()
        {
            var pencil = new Ticonderoga(10, 20, 30);
            Assert.AreEqual(pencil.LetterCapacityPerSharpening, 10);
            Assert.AreEqual(pencil.MaximumNumberOfSharpenings, 20);
            Assert.AreEqual(pencil.EraserLetterCapacity, 30);
        }

        [TestMethod, TestCategory("Pencil Creation")]
        public void Constructor_NegativeLetterAmount_Throws()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Ticonderoga(-10, 20, 30), "Letter capacity must be greater than 0"); 
        }

        [TestMethod, TestCategory("Pencil Creation")]
        public void Constructor_NegativeSharpeningAmount_Throws()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Ticonderoga(10, -20, 30), "Maximum sharpenings must be greater than 0");
        }

        [TestMethod, TestCategory("Pencil Creation")]
        public void Constructor_NegativeEraserAmount_Throws()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Ticonderoga(10, 20, -30), "Eraser Capacity must be greater than 0");
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void SharpenPencil_WhenMaxSharpeningsReached_Throws()
        {
            var pencil = new Ticonderoga(10, 1, 20);
            pencil.Sharpen();
            Assert.ThrowsException<Exception>(() => pencil.Sharpen(), "Pencil can not be sharpened anymore. Please buy a new pencil.");
        }

        [TestMethod, TestCategory("Pencil Sharpen")]
        public void SharpenPencil_WhenSharpened_IncreasesCurrentSharpeningCount()
        {
            var pencil = new Ticonderoga(10, 10, 20);
            pencil.Sharpen();
            Assert.AreEqual(1, pencil.CurrentSharpeningCount); 
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Write_WhenGivenALetterWithCapacity_WillReturnThatLetter()
        {
            var pencil = new Ticonderoga(10, 10, 20);
            var output = pencil.Write('a'); 
            Assert.AreEqual('a', output);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Write_WhenGivenASpaceWithCapacity_WillReturnASpace()
        {
            var pencil = new Ticonderoga(10, 10, 20);
            var output = pencil.Write(' ');
            Assert.AreEqual(' ', output);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Write_WhenGivenALetterWithNoCapacity_WillReturnASpace()
        {
            var pencil = new Ticonderoga(1, 10, 20);
            var firstOutput = pencil.Write('a');
            var secondOutput = pencil.Write('b');

            Assert.AreEqual('a', firstOutput);
            Assert.AreEqual(' ', secondOutput);
        }

        [TestMethod, TestCategory("Pencil Write")]
        public void Write_InputCharAsASpace_DoesNotIncreaseLetterCount()
        {
            var pencil = new Ticonderoga(1, 10, 20);
            var output = pencil.Write(' ');           

            Assert.AreEqual(0, pencil.CurrentLetterCount);
        }
    }
}
