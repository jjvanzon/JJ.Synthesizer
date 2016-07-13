using System.Collections.Generic;

namespace JJ.Data.Synthesizer
{
    public class Channel
    {
        public Channel()
        {
            SpeakerSetupChannels = new List<SpeakerSetupChannel>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        /// <summary>
        /// bridge entity
        /// </summary>
        public virtual IList<SpeakerSetupChannel> SpeakerSetupChannels { get; set; }
    }
}
