namespace DLS.LBXLoader.Tests
{
    using NUnit.Framework;
    using System;

    class HeaderTests
    {
        [TestFixture]
        class Constructor
        {
            private byte[] _properUnpaddedTestHeader;
            private byte[] _badSignatureTestHeader;
            private byte[] _tooShortHeader;
            private UInt32[] _testOffsets = new uint[]
            {
                10, 20, 30, 40
            };

            [SetUp]
            public void GenerateTestHeader()
            {
                _properUnpaddedTestHeader = BuildTestHeader(_testOffsets);
                _badSignatureTestHeader = BuildTestHeader(_testOffsets, 0, 0, new byte[] { 0x00, 0x00, 0x00, 0x00 });
                _tooShortHeader = new byte[6];
            }

            [Test]
            public void FileCountIsProperyRead()
            {
                var header = new Header(_properUnpaddedTestHeader);

                Assert.That(header.NumFiles, Is.EqualTo(_testOffsets.Length));
            }

            [Test]
            public void FileOffsetsProperlyRead()
            {
                var header = new Header(_properUnpaddedTestHeader);

                Assert.That(header.FileOffsets, Is.EqualTo(_testOffsets));
            }

            [Test]
            public void InvalidSignatureThrows()
            {
                Assert.That(() => new Header(_badSignatureTestHeader), Throws.Exception);
            }

            [Test]
            public void ArrayTooShortThrows()
            {
                Assert.That(() => new Header(_tooShortHeader), Throws.Exception);
            }

            private Byte[] BuildTestHeader(UInt32[] fileOffsets, uint leftPadding=0, uint rightPadding=0, byte[] signature=null)
            {
                signature = signature ?? new byte[] { 0xAD, 0xFE, 0x00, 0x00 };
                var totalSize = 8 + fileOffsets.Length * 4 + leftPadding + rightPadding;
                var array = new byte[totalSize];
                var numFiles = Convert.ToUInt16(fileOffsets.Length);
                UInt16 info = new ushort();

                Array.Copy(BitConverter.GetBytes(numFiles), 0, array, 0, 2);
                Array.Copy(signature, 0, array, 2, 4);
                Array.Copy(BitConverter.GetBytes(info), 0, array, 6, 2);
                for (var i = 0; i < fileOffsets.Length; i++)
                {
                    Array.Copy(BitConverter.GetBytes(fileOffsets[i]), 0, array, 8 + i * 4, 4);
                }

                return array;
            }
        }
    }
}
