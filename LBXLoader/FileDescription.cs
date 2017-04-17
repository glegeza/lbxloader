
namespace DLS.LBXLoader
{
    using System;
    using System.Text;

    public class FileDescription
    {
        private static readonly int NAME_LENGTH = 9;
        private static readonly int DESC_LENGTH = 23;

        public string Name { get; private set; }
        public string Description { get; private set; }

        public FileDescription(Byte[] file, int offset=0)
        {
            Name = Encoding.ASCII.GetString(file, offset, NAME_LENGTH);
            Description = Encoding.ASCII.GetString(file, offset + NAME_LENGTH, DESC_LENGTH);
        }
    }
}
