namespace DLS.LBXLoader
{
    using System;
    using System.Collections.Generic;

    public class Container
    {
        /// <summary>
        /// Reads the contents of an LBX file from an array of bytes.
        /// </summary>
        /// <param name="file">An array of bytes containing an LBX file.
        /// </param>
        public Container(Byte[] file)
        {
            var header = new Header(file);
            var fileDescs = new List<FileDescription>();

            var fileDescRange = header.FileOffsets[0] - 512;
            for (var i = 512; i < header.FileOffsets[0]; i += 32)
            {
                var desc = new FileDescription(file, i);
                fileDescs.Add(desc);
            }

            var packedFiles = new List<PackedFile>();
            for (var i = 0; i < header.FileOffsets.Length - 1; i++)
            {
                var size = header.FileOffsets[i + 1] - header.FileOffsets[i];
                FileDescription desc = null;
                if (i < fileDescs.Count)
                {
                    desc = fileDescs[i];
                }
                var packedFile = new PackedFile(file, header.FileOffsets[i], size, desc);
                packedFiles.Add(packedFile);
            }

            Files = packedFiles;
        }

        public IEnumerable<PackedFile> Files { get; private set; }
    }
}
