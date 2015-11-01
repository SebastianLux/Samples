using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace slux.Security.Cryptography.Tests
{
    [TestClass]
    public class Crc32Tests
    {
        [TestMethod]
        public void ShouldDifferOnBitSwitch()
        {
            var crc32 = new Crc32();

            var hash1 = crc32.ComputeHash(Encoding.Default.GetBytes("00001"));

            var hash2 = crc32.ComputeHash(Encoding.Default.GetBytes("00010"));

            CollectionAssert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void ShouldReturnSameHashForMultipleCalls()
        {
            var crc32 = new Crc32();

            byte[] previous = null;

            for (int i = 0; i < 100; i++)
            {
                var actual = crc32.ComputeHash(
                    Encoding.Default.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris porta ex quis volutpat hendrerit. Vivamus consequat venenatis velit sit amet varius."));

                if (previous != null)
                {
                    CollectionAssert.AreEqual(previous, actual);            
                }

                previous = actual;
            }
        }
    }
}
