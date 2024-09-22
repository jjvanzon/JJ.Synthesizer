using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Infos
{
    public class AudioFileInfo
    {
        public int BytesPerValue { get; set; }
        public int ChannelCount { get; set; }
        public int SampleCount { get; set; }
        public int SamplingRate { get; set; }
    }
}
