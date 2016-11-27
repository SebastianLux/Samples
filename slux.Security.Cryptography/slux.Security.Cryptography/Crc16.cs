using System;
using System.Security.Cryptography;

namespace slux.Security.Cryptography
{
    public class Crc16 : HashAlgorithm
    {
        private readonly object syncLock = new object();

        // ISO/IEC13239 CRC16
        private readonly UInt16 polynomial = 0x8408;
        private UInt16 hash;
        private readonly UInt16[] table;


        public Crc16()
        {
            this.table = InitializeTable();
        }

        public override void Initialize()
        {
          
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            //ushort crc = 0xFFFF;
            //for (int i = 0; i < array.Length; ++i)
            //{
            //    byte index = (byte)(crc ^ array[i]);
            //    crc = (ushort)((crc >> 8) ^ table[index]);
            //}
            //this.hash = (ushort) ~crc;

            //ushort fcs = 0xFFFF;

            //int end = ibStart + cbSize;

            //for (int i = ibStart; i < end; i++)
            //{
            //    fcs = (ushort)(((ushort)(fcs >> 8)) ^ table[(fcs ^ array[i]) & 0xFF]);
            //}

            //this.hash = (ushort) ~fcs;
            unchecked
            {
                ushort computedHash = (ushort) -1;



                for (var i = ibStart; i < cbSize; i++)
                {
                    unchecked
                    {
                        computedHash = (ushort) ((computedHash >> 8) ^ this.table[array[i] ^ computedHash & 0xff]);
                    }
                }

                this.hash = (ushort) ~computedHash;
            }
        }

        protected override byte[] HashFinal()
        {
            lock (this.syncLock)
            {
                return new byte[]
                       {
                           (byte) ((this.hash >> 8) & 0xff),
                           (byte) (this.hash & 0xff)
                       };
            }
        }

        public UInt16[] InitializeTable()
        {
            var createTable = new UInt16[256];

            for (var i = 0; i < 256; i++)
            {
                var entry = (UInt16)i;

                for (var j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                        entry = (UInt16) ((entry >> 1) ^ this.polynomial);
                    else
                        entry = (UInt16) (entry >> 1);
                }

                createTable[i] = entry;
            }

            return createTable;
        }
    }
}