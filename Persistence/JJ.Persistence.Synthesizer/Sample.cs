using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class Sample
    {
        public Sample ()
	    {
            SampleChannels = new List<SampleChannel>();
	    }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual double Amplifier { get; set; }
        public virtual double TimeMultiplier { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int BytesToSkip { get; set; }
        public virtual string Location { get; set; }
        public virtual int BitsPerSample { get; set; }

        public virtual ChannelSetup ChannelSetup { get; set; }
        public virtual SampleFormat SampleFormat { get; set; }
        public virtual InterpolationType InterpolationType { get; set; }

        public virtual IList<SampleChannel> SampleChannels { get; set; }
    }
}
