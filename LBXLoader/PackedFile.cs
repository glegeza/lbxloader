namespace DLS.LBXLoader
{
    using System;

    public class PackedFile
    {
        public FileDescription Description { get; private set; }
        public byte[] FileBytes { get; private set; }
        public uint Size { get; private set; }

        public PackedFile(byte[] file, uint offset, uint size, FileDescription desc)
        {
            FileBytes = new byte[size];
            Array.Copy(file, offset, FileBytes, 0, size);
            Description = desc;
            Size = size;
        }
    }
}
