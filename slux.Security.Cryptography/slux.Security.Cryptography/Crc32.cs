using System;
using System.Security.Cryptography;

namespace slux.Security.Cryptography
{
    /// <summary>
    /// Computes the 32 bit cyclic redundancy check hash value of the input data.
    /// </summary>
    /// <remarks>This class can not be inherited.</remarks>
    public sealed class Crc32 : HashAlgorithm
    {
        private UInt32 hash;
        private readonly UInt32 polynomial = 0xedb88320;
        private readonly object syncLock = new object();
        private readonly UInt32[] table;

        /// <summary>
        /// Creates a new instance of the <see cref="Crc32"/> class.
        /// </summary>
        public Crc32()
        {
            this.table = InitializeTable(); 
        }

        /// <summary>
        /// Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm"/> class.
        /// </summary>
        public override void Initialize()
        {
            // Not implemented.
        }


        /// <summary>
        /// When overridden in a derived class, routes data written to the object into the hash algorithm for computing the hash.
        /// </summary>
        /// <param name="array">The input to compute the hash code for. </param><param name="ibStart">The offset into the byte array from which to begin using data. </param>
        /// <param name="cbSize">The number of bytes in the byte array to use as data. </param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            lock (this.syncLock)
            {
                this.hash = Calculate(array, ibStart, cbSize);
            }
        }

        /// <summary>
        /// When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.
        /// </summary>
        /// <returns>
        /// The computed hash code.
        /// </returns>
        protected override byte[] HashFinal()
        {
            lock (this.syncLock)
            {
                return new byte[]
                       {
                           (byte) ((this.hash >> 24) & 0xff),
                           (byte) ((this.hash >> 16) & 0xff),
                           (byte) ((this.hash >> 8) & 0xff),
                           (byte) (this.hash & 0xff)
                       };
            }
        }

        /// <summary>
        /// Gets the size, in bits, of the computed hash code.
        /// </summary>
        /// <returns>
        /// The size, in bits, of the computed hash code.
        /// </returns>
        public override int HashSize
        {
            get { return 32; }
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

        private UInt32 Calculate(byte[] array, int start, int size)
        {
            var crc = 0xffffffff;

            for (var i = start; i < size; i++)
            {
                unchecked
                {
                    crc = (crc >> 8) ^ this.table[array[i] ^ crc & 0xff];
                }
            }

            return ~crc;
        }
    }
}
