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
    }
}
