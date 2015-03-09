using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Converters
{
    internal static class AudioFileInfoToWavHeaderStructConverter
    {
        public static WavHeaderStruct Convert(AudioFileInfo audioFileInfo)
        {
            if (audioFileInfo == null) throw new NullException(() => audioFileInfo);

            int soundByteCount = audioFileInfo.SampleCount * audioFileInfo.ChannelCount * audioFileInfo.BytesPerValue;
            
            WavHeaderStruct wavHeaderStruct = new WavHeaderStruct
            {
                ChunkID = WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_RIFF,

                // Total size of file minus ChunkID and ChunkSize.
                ChunkSize = WavHeaderConstants.WAV_HEADER_LENGTH - sizeof(int) * 2 + soundByteCount,
                Format = WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_WAVE,

                SubChunk1ID = WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_FMT_SPACE_LOWERCASE,
                SubChunk1Size = WavHeaderConstants.SUB_CHUNK_1_SIZE,
                AudioFormat = WavHeaderConstants.AUDIO_FORMAT_INDICATOR_PCM,
                ChannelCount = (short)audioFileInfo.ChannelCount,
                SamplingRate = audioFileInfo.SamplingRate,
                BytesPerSecond = audioFileInfo.ChannelCount * audioFileInfo.BytesPerValue * audioFileInfo.SamplingRate,
                BytesPerSample = (short)(audioFileInfo.ChannelCount * audioFileInfo.BytesPerValue),
                BitsPerValue = (short)(audioFileInfo.BytesPerValue * 8),

                SubChunk2ID = WavHeaderConstants.BIG_ENDIAN_ASCII_CHARACTERS_DATA_LOWERCASE,
                SubChunk2Size = soundByteCount
            };

            return wavHeaderStruct;
        }
    }
}
