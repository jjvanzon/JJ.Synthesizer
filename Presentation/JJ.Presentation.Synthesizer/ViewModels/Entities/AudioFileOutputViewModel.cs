using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class AudioFileOutputViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Amplifier { get; set; }
        public double TimeMultiplier { get; set; }
        public double StartTime { get; set; }
        public double Duration { get; set; }
        public int SamplingRate { get; set; }
        public string FilePath { get; set; }
        public IDName AudioFileFormat { get; set; }
        public IDName SampleDataType { get; set; }
        public IDName SpeakerSetup { get; set; }

        public IList<AudioFileOutputChannelViewModel> AudioFileOutputChannels { get; set; }
    }
}
