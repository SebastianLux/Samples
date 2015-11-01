using System;

namespace slux.Security.Cryptography
{
    /// <summary>
    /// Computes the 32 bit Adler hash value of the input data.
    /// </summary>
    /// <remarks>This class can not be inherited.</remarks>
    public sealed class Adler32 : UInt32HashAlgorithmBase
    {
        protected override UInt32 Compute(byte[] array, Int32 start, Int32 size)
        {
            UInt32 computedHash = 1;
            UInt32 s1 = computedHash & 0xFFFF;
            UInt32 s2 = computedHash >> 16;

            while (size > 0)
            {
                var n = (3800 > size) ? size : 3800;
                size -= n;

                while (--n >= 0)
                {
                    s1 = s1 + (UInt32)(array[start++] & 0xFF);
                    s2 = s2 + s1;
                }

                s1 %= 65521;
                s2 %= 65521;
            }

            computedHash = (s2 << 16) | s1;

            return ~computedHash;
        }
    }
}
