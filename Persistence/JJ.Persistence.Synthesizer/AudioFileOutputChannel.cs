using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class AudioFileOutputChannel
    {
        public int ID { get; set; }

        /// <summary>
        /// nullable
        /// </summary>
        public Outlet Outlet { get; set; }

        public int Index { get; set; }

        /// <summary>
        /// parent, not nullable
        /// </summary>
        public AudioFileOutput AudioFileOutput { get; set; }
    }
}
