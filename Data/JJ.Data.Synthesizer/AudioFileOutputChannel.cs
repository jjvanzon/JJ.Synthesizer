using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    public class AudioFileOutputChannel
    {
        public virtual int ID { get; set; }

        /// <summary>
        /// nullable
        /// </summary>
        public virtual Outlet Outlet { get; set; }

        public virtual int IndexNumber { get; set; }

        /// <summary>
        /// parent, not nullable
        /// </summary>
        public virtual AudioFileOutput AudioFileOutput { get; set; }
    }
}
