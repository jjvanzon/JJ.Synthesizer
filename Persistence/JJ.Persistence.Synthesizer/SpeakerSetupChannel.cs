using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    /// <summary>
    /// bridge entity
    /// </summary>
    public class SpeakerSetupChannel
    {
        public virtual int ID { get; set; }
        public virtual int Index { get; set; }

        /// <summary>
        /// not nullable
        /// </summary>
        public virtual SpeakerSetup SpeakerSetup { get; set; }

        /// <summary>
        /// not nullable
        /// </summary>
        public virtual Channel Channel { get; set; }
    }
}
