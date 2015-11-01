using System;
using System.Security.Cryptography;

namespace slux.Security.Cryptography
{
    public sealed class Crc32 : HashAlgorithm
    {
        private UInt32 hash;
        private readonly UInt32 polynomial = 0xedb88320;
        private readonly UInt32[] table;

        public Crc32()
        {
            this.table = InitializeTable(); 
        }

        public override void Initialize()
        {
            // Not implemented.
        }


        protected override void HashCore(byte[] buffer, int start, int length)
        {
            this.hash = Calculate(buffer, start, length);
        }

        protected override byte[] HashFinal()
        {
            return new byte[] {
					(byte)((this.hash >> 24) & 0xff),
					(byte)((this.hash >> 16) & 0xff),
					(byte)((this.hash >> 8) & 0xff),
					(byte)(this.hash & 0xff)
				};
        }

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

        private UInt32 Calculate(byte[] buffer, int start, int size)
        {
            var crc = 0xffffffff;

            for (var i = start; i < size; i++)
            {
                unchecked
                {
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                }
            }

            return ~crc;
        }
    }
}
