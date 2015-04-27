using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    public class AudioFileOutput
    {
        public AudioFileOutput()
        {
            AudioFileOutputChannels = new List<AudioFileOutputChannel>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual double Amplifier { get; set; }
        public virtual double TimeMultiplier { get; set; }
        public virtual double StartTime { get; set; }
        public virtual double Duration { get; set; }
        public virtual int SamplingRate { get; set; }
        public virtual string FilePath { get; set; }

        public virtual AudioFileFormat AudioFileFormat { get; set; }
        public virtual SampleDataType SampleDataType { get; set; }
        public virtual SpeakerSetup SpeakerSetup { get; set; }

        public virtual IList<AudioFileOutputChannel> AudioFileOutputChannels { get; set; }

        /// <summary> parent, nullable </summary>
        public virtual Document Document { get; set; }
    }
}
