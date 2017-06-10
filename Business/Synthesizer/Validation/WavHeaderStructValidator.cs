using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class WavHeaderStructValidator : VersatileValidator<WavHeaderStruct>
    {
        public WavHeaderStructValidator(WavHeaderStruct wavHeaderStruct)
            : base(wavHeaderStruct)
        { 
            For(() => wavHeaderStruct.ChunkID, nameof(wavHeaderStruct.ChunkID)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_RIFF);
            For(() => wavHeaderStruct.Format, nameof(wavHeaderStruct.Format)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_WAVE);
            For(() => wavHeaderStruct.SubChunk1ID, nameof(wavHeaderStruct.SubChunk1ID)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_FMT_SPACE_LOWERCASE);
            For(() => wavHeaderStruct.SubChunk1Size, nameof(wavHeaderStruct.SubChunk1Size)).Is(WavHeaderConstants.SUB_CHUNK_1_SIZE);
            For(() => wavHeaderStruct.AudioFormat, nameof(wavHeaderStruct.AudioFormat)).Is(WavHeaderConstants.AUDIO_FORMAT_INDICATOR_PCM);
            For(() => wavHeaderStruct.SubChunk2ID, nameof(wavHeaderStruct.SubChunk2ID)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_DATA_LOWERCASE);

            // For the moment only support 1 or 2 channels
            For(() => (int)wavHeaderStruct.ChannelCount, nameof(wavHeaderStruct.ChannelCount)).GreaterThanOrEqual(1).LessThanOrEqual(2);

            // For the moment only support 8 bit and 16 bit integers.
            For(() => (int)wavHeaderStruct.BitsPerValue, nameof(wavHeaderStruct.BitsPerValue)).In(8, 16);

            For(() => wavHeaderStruct.SamplingRate, nameof(wavHeaderStruct.SamplingRate)).GreaterThan(0);

            For(() => (int)wavHeaderStruct.BytesPerSample, nameof(wavHeaderStruct.BytesPerSample))
                .Is(wavHeaderStruct.ChannelCount * wavHeaderStruct.BitsPerValue / 8);

            For(() => wavHeaderStruct.BytesPerSecond, nameof(wavHeaderStruct.BytesPerSecond))
                .Is(wavHeaderStruct.ChannelCount * wavHeaderStruct.BitsPerValue / 8 * wavHeaderStruct.SamplingRate);

            // ChunkSize = total size of file minus ChunkID and ChunkSize.
            For(() => wavHeaderStruct.ChunkSize, nameof(wavHeaderStruct.ChunkSize))
                .Is(WavHeaderConstants.WAV_HEADER_LENGTH - sizeof(int) * 2 + wavHeaderStruct.SubChunk2Size);
        }
    }
}
