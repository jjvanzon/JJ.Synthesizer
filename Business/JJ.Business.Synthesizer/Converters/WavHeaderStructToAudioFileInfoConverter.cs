using JJ.Business.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Converters
{
    internal static class WavHeaderStructToAudioFileInfoConverter
    {
        public static AudioFileInfo Convert(WavHeaderStruct wavHeaderStruct)
        {
            if (wavHeaderStruct.ChannelCount == 0) throw new Exception("wavHeaderStruct.ChannelCount cannot be 0.");
            if (wavHeaderStruct.BitsPerValue == 0) throw new Exception("wavHeaderStruct.BitsPerValue cannot be 0.");

            var audioFileInfo = new AudioFileInfo
            {
                BytesPerValue = wavHeaderStruct.BitsPerValue / 8,
                ChannelCount = wavHeaderStruct.ChannelCount,
                SamplingRate = wavHeaderStruct.SamplingRate,
                SampleCount = wavHeaderStruct.SubChunk2Size / wavHeaderStruct.ChannelCount / (wavHeaderStruct.BitsPerValue / 8)
            };

            return audioFileInfo;
        }
    }
}
