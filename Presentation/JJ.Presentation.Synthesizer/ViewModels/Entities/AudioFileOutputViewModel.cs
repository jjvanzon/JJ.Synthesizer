using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class AudioFileOutputViewModel
    {
        public AudioFileOutputKeysViewModel Keys { get; set; }

        public string Name { get; set; }

        public int SamplingRate { get; set; }
        public IDAndName AudioFileFormat { get; set; }
        public IDAndName SampleDataType { get; set; }
        public IDAndName SpeakerSetup { get; set; }

        public double StartTime { get; set; }
        public double Duration { get; set; }
        public double Amplifier { get; set; }
        public double TimeMultiplier { get; set; }

        public string FilePath { get; set; }

        public IList<AudioFileOutputChannelViewModel> Channels { get; set; }
    }
}
