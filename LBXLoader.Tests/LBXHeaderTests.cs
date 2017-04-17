namespace DLS.LBXLoader.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    class LBXHeaderTests
    {
        [TestFixture]
        class ConstructorTest
        {
            [Test]
            private void FileCountIsProperyRead()
            {

            }

            [Test]
            private void SignatureIsProperlyRead()
            {

            }

            [Test]
            private void FileOffsetsProperlyRead()
            {

            }

            [Test]
            private void InvalidSignatureThrows()
            {

            }

            [Test]
            private void ArrayTooShortThrows()
            {

            }

            private Byte[] BuildTestHeader(uint totalSize, UInt32[] fileOffsets)
            {
                var array = new byte[totalSize];

                return array;
            }
        }
    }
}
