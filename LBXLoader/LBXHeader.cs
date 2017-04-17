namespace DLS.LBXLoader
{
    using System;
    using System.Linq;

    public class LBXHeader
    {
        private static readonly int NUM_FILES_OFFSET = 0;
        private static readonly int MAGIC_OFFSET = 2;
        private static readonly int INFO_OFFSET = 6;
        private static readonly int OFFSETLIST_OFFSET = 8;

        public static readonly byte[] MAGIC = { 0xAD, 0xFE, 0x00, 0x00 };

        public UInt16 NumFiles { get; private set; }
        public byte[] Magic { get; private set; }
        public UInt16 Information { get; private set; }
        public UInt32[] FileOffsets { get; private set; }
        public UInt32[] EOFOffset { get; private set; }

        public LBXHeader(byte[] file, int startOffset=0)
        {
            if (file.Length - startOffset < OFFSETLIST_OFFSET)
            {
                throw new ArgumentException("Byte array too short for " +
                    "proper header definition.");
            }

            Magic = file.Skip(startOffset + MAGIC_OFFSET).Take(4).ToArray();
            if (!Enumerable.SequenceEqual(Magic, MAGIC))
            {
                throw new ArgumentException("Invalid signature. Byte array is" +
                    "not a proper LBX container.");
            }


            NumFiles = BitConverter.ToUInt16(file, startOffset + NUM_FILES_OFFSET);
            FileOffsets = new uint[NumFiles];

            var offsetListStart = startOffset + OFFSETLIST_OFFSET;
            for (var i = 0; i < NumFiles; i++)
            {
                var pos = offsetListStart + i * 4;
                FileOffsets[i] = BitConverter.ToUInt32(file, pos);
            }
        }
    }
}
