using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Helpers
{
    /// <summary>
    /// This page describes it pretty well:
    /// http://www-mmsp.ece.mcgill.ca/documents/AudioFormats/WAVE/WAVE.html
    /// </summary>
    public struct WavHeaderStruct
    {
        public int ChunkID;
        public int ChunkSize;
        public int Format;

        public int SubChunk1ID;
        public int SubChunk1Size;
        public short AudioFormat;
        public short ChannelCount;
        public int SamplingRate;
        public int BytesPerSecond;
        public short BytesPerSample;
        public short BitsPerValue;

        public int SubChunk2ID;
        public int SubChunk2Size;
    }
}
