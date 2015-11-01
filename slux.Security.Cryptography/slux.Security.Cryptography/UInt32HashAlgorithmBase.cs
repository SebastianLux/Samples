using System;
using System.Security.Cryptography;

namespace slux.Security.Cryptography
{
    /// <summary>
    /// Represents the base class from which all implementations of a <see cref="UInt32"/> cryptographic hash algorithms must derive.
    /// </summary>
    public abstract class UInt32HashAlgorithmBase : HashAlgorithm
    {
        private UInt32 hash;
        private readonly object syncLock = new object();

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

        /// <summary>
        /// Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm"/> class.
        /// </summary>
        public override void Initialize()
        {

        }

        /// <summary>
        /// When overridden in a derived class, routes data written to the object into the hash algorithm for computing the hash.
        /// </summary>
        /// <param name="array">The input to compute the hash code for. </param><param name="ibStart">The offset into the byte array from which to begin using data.</param>
        /// <param name="cbSize">The number of bytes in the byte array to use as data. </param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            lock (this.syncLock)
            {
                this.hash = Compute(array, ibStart, cbSize);
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
        /// When overridden in a derived class,, computes the hash value.
        /// </summary>
        /// <param name="array">The input to compute the hash code for.</param>
        /// <param name="start">The offset into the byte array from which to begin using data.</param>
        /// <param name="size">The number of bytes in the byte array to use as data. </param>
        /// <returns></returns>
        protected abstract UInt32 Compute(byte[] array, Int32 start, Int32 size);
    }
}
