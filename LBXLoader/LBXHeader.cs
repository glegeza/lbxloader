namespace DLS.LBXLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class LBXHeader
    {
        public UInt16 NumFiles { get; private set; }
        public Byte Magic { get; private set; }
        public UInt16 Information { get; private set; }
        public UInt32[] FileOffsets { get; private set; }
        public UInt32[] EOFOffset { get; private set; }


    }
}
