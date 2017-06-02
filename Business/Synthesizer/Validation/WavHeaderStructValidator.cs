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
            For(() => Obj.ChunkID, nameof(Obj.ChunkID)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_RIFF);
            For(() => Obj.Format, nameof(Obj.Format)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_WAVE);
            For(() => Obj.SubChunk1ID, nameof(Obj.SubChunk1ID)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_FMT_SPACE_LOWERCASE);
            For(() => Obj.SubChunk1Size, nameof(Obj.SubChunk1Size)).Is(WavHeaderConstants.SUB_CHUNK_1_SIZE);
            For(() => Obj.AudioFormat, nameof(Obj.AudioFormat)).Is(WavHeaderConstants.AUDIO_FORMAT_INDICATOR_PCM);
            For(() => Obj.SubChunk2ID, nameof(Obj.SubChunk2ID)).Is(WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_DATA_LOWERCASE);

            // For the moment only support 1 or 2 channels
            For(() => (int)Obj.ChannelCount, nameof(Obj.ChannelCount)).GreaterThanOrEqual(1).LessThanOrEqual(2);

            // For the moment only support 8 bit and 16 bit integers.
            For(() => (int)Obj.BitsPerValue, nameof(Obj.BitsPerValue)).In(8, 16);

            For(() => Obj.SamplingRate, nameof(Obj.SamplingRate)).GreaterThan(0);

            For(() => (int)Obj.BytesPerSample, nameof(Obj.BytesPerSample))
                .Is(Obj.ChannelCount * Obj.BitsPerValue / 8);

            For(() => Obj.BytesPerSecond, nameof(Obj.BytesPerSecond))
                .Is(Obj.ChannelCount * Obj.BitsPerValue / 8 * Obj.SamplingRate);

            // ChunkSize = total size of file minus ChunkID and ChunkSize.
            For(() => Obj.ChunkSize, nameof(Obj.ChunkSize))
                .Is(WavHeaderConstants.WAV_HEADER_LENGTH - sizeof(int) * 2 + Obj.SubChunk2Size);
        }
    }
}
