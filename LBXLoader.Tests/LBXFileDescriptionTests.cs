namespace DLS.LBXLoader.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    class LBXFileDescriptionTests
    {
        [TestFixture]
        class ConstructorTest
        {
            [Test]
            public void LoadProperlyFormattedRepeatingCharactersFromBeginningOfArray()
            {
                var nameStr = new String('T', 9);
                var descStr = new String('T', 23);
                var fullArray = BuildArray(32, 0, nameStr, descStr);

                var testDesc = new LBXFileDescription(fullArray);

                Assert.That(testDesc.Name, Is.EqualTo(nameStr));
                Assert.That(testDesc.Description, Is.EqualTo(descStr));
            }

            [Test]
            public void LoadProperyFormattedRepeatingCharactersFromMiddleOfArray()
            {
                var offset = 20;
                var length = 100;
                var nameStr = new String('T', 9);
                var descStr = new String('T', 23);
                var fullArray = BuildArray(length, offset, nameStr, descStr);

                var testDesc = new LBXFileDescription(fullArray, offset);

                Assert.That(testDesc.Name, Is.EqualTo(nameStr));
                Assert.That(testDesc.Description, Is.EqualTo(descStr));
            }

            [Test]
            public void LoadingTooSmallBytesArrayThrows()
            {
                var fullArray = BuildArray(8, 0, "T", "T");

                Assert.That(() => new LBXFileDescription(fullArray), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
            }

            private Byte[] BuildArray(int size, int offset, string name, string desc)
            {
                var array = new byte[size];
                var nameBytes = Encoding.ASCII.GetBytes(name);
                var descBytes = Encoding.ASCII.GetBytes(desc);
                Array.Copy(nameBytes, 0, array, offset, nameBytes.Length);
                Array.Copy(descBytes, 0, array, offset + nameBytes.Length, descBytes.Length);

                return array;
            }
        }
    }
}
