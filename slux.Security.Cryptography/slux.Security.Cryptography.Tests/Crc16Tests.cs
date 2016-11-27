using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace slux.Security.Cryptography.Tests
{
    [TestClass]
    public class Crc16Tests
    {
        [TestMethod]
        public void ShouldComputeCrc16()
        {
            var crc16 = new Crc16();

            // ISO/IEC13239: This should lead to the CRC 0x52ED
            byte[] source =
            {
                0x02, 0x07, 0x01, 0x03, 0x01, 0x02, 0x00, 0x34, 0x07, 0x07, 0x1C, 0x59, 0x34, 0x6F, 0xE1,
                0x83, 0x00, 0x00, 0x41, 0x06, 0x06, 0x7B, 0x3C, 0xFF, 0xCF, 0x3C, 0xC0
            };

            var hash1 = crc16.ComputeHash(source);

            Assert.AreEqual("52ED", BitConverter.ToString(hash1).Replace("-", ""));
        }

    }
}
