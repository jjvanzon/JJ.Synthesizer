using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    internal class WavHeaderStructValidator : FluentValidator<WavHeaderStruct>
    {
        public WavHeaderStructValidator(WavHeaderStruct obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.ChunkID, PropertyNames.ChunkID).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_RIFF);
            For(() => Object.Format, PropertyNames.Format).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_WAVE);
            For(() => Object.SubChunk1ID, PropertyNames.SubChunkID).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_FMT_SPACE_LOWERCASE);
            For(() => Object.SubChunk1Size, PropertyNames.SubChunk1Size).Is(WavHeaderConstants.SUB_CHUNK_1_SIZE);
            For(() => Object.AudioFormat, PropertyNames.AudioFormat).Is(WavHeaderConstants.AUDIO_FORMAT_INDICATOR_PCM);
            For(() => Object.SubChunk2ID, PropertyNames.SubChunk2ID).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_DATA_LOWERCASE);

            // For the moment only support 1 or 2 channels
            For(() => (int)Object.ChannelCount, PropertyNames.ChannelCount).Min(1).Max(2);

            // For the moment only support 8 bit and 16 bit integers.
            For(() => (int)Object.BitsPerValue, PropertyNames.BitsPerValue).In(8, 16);

            For(() => Object.SamplingRate, PropertyNames.SamplingRate).Above(0);

            For(() => (int)Object.BytesPerSample, PropertyNames.BytesPerSample)
                .Is(Object.ChannelCount * Object.BitsPerValue / 8);

            For(() => (int)Object.BytesPerSecond, PropertyNames.BytesPerSecond)
                .Is(Object.ChannelCount * Object.BitsPerValue / 8 * Object.SamplingRate);

            // ChunkSize = total size of file minus ChunkID and ChunkSize.
            For(() => Object.ChunkSize, PropertyNames.ChunkSize)
                .Is(WavHeaderConstants.WAV_HEADER_LENGTH - sizeof(int) * 2 + Object.SubChunk2Size);
        }
    }
}
