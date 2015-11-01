using System;

namespace slux.Security.Cryptography
{
    /// <summary>
    /// Computes the 32 bit cyclic redundancy check hash value of the input data.
    /// </summary>
    /// <remarks>This class can not be inherited.</remarks>
    public sealed class Crc32 : UInt32HashAlgorithmBase
    {
        private readonly UInt32 polynomial = 0xedb88320;
        private readonly UInt32[] table;

        /// <summary>
        /// Creates a new instance of the <see cref="Crc32"/> class.
        /// </summary>
        public Crc32()
        {
            this.table = InitializeTable(); 
        }

        /// <summary>
        /// When overridden in a derived class,, computes the hash value.
        /// </summary>
        /// <param name="array">The input to compute the hash code for.</param>
        /// <param name="start">The offset into the byte array from which to begin using data.</param>
        /// <param name="size">The number of bytes in the byte array to use as data. </param>
        /// <returns></returns>
        protected override UInt32 Compute(byte[] array, int start, int size)
        {
            UInt32 computedHash = 0xffffffff;

            for (var i = start; i < size; i++)
            {
                unchecked
                {
                    computedHash = (computedHash >> 8) ^ this.table[array[i] ^ computedHash & 0xff];
                }
            }

            return ~computedHash;
        }

        private UInt32[] InitializeTable()
        {
            var createTable = new UInt32[256];

            for (var i = 0; i < 256; i++)
            {
                var entry = (UInt32)i;

                for (var j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ this.polynomial;
                    else
                        entry = entry >> 1;
                }

                createTable[i] = entry;
            }

            return createTable;
        }
    }
}
