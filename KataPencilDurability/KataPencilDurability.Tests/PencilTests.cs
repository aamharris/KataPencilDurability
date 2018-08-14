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
    }
}
