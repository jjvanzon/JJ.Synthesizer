using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class Outlet
    {
        public Outlet()
        {
            ConnectedInlets = new List<Inlet>();
            AsAudioFileOutputChannels = new List<AudioFileOutputChannel>();
        }

        public virtual int ID { get; set; }

        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Inlet> ConnectedInlets { get; set; }

        public virtual IList<AudioFileOutputChannel> AsAudioFileOutputChannels { get; set; }
    }
}
