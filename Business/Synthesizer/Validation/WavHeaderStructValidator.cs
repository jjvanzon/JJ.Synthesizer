using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class WavHeaderStructValidator : VersatileValidator<WavHeaderStruct>
    {
        public WavHeaderStructValidator(WavHeaderStruct obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.ChunkID, PropertyNames.ChunkID).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_RIFF);
            For(() => Obj.Format, PropertyNames.Format).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_WAVE);
            For(() => Obj.SubChunk1ID, PropertyNames.SubChunkID).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_FMT_SPACE_LOWERCASE);
            For(() => Obj.SubChunk1Size, PropertyNames.SubChunk1Size).Is(WavHeaderConstants.SUB_CHUNK_1_SIZE);
            For(() => Obj.AudioFormat, PropertyNames.AudioFormat).Is(WavHeaderConstants.AUDIO_FORMAT_INDICATOR_PCM);
            For(() => Obj.SubChunk2ID, PropertyNames.SubChunk2ID).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_DATA_LOWERCASE);

            // For the moment only support 1 or 2 channels
            For(() => (int)Obj.ChannelCount, PropertyNames.ChannelCount).GreaterThanOrEqual(1).LessThanOrEqual(2);

            // For the moment only support 8 bit and 16 bit integers.
            For(() => (int)Obj.BitsPerValue, PropertyNames.BitsPerValue).In(8, 16);

            For(() => Obj.SamplingRate, PropertyNames.SamplingRate).GreaterThan(0);

            For(() => (int)Obj.BytesPerSample, PropertyNames.BytesPerSample)
                .Is(Obj.ChannelCount * Obj.BitsPerValue / 8);

            For(() => Obj.BytesPerSecond, PropertyNames.BytesPerSecond)
                .Is(Obj.ChannelCount * Obj.BitsPerValue / 8 * Obj.SamplingRate);

            // ChunkSize = total size of file minus ChunkID and ChunkSize.
            For(() => Obj.ChunkSize, PropertyNames.ChunkSize)
                .Is(WavHeaderConstants.WAV_HEADER_LENGTH - sizeof(int) * 2 + Obj.SubChunk2Size);
        }
    }
}
