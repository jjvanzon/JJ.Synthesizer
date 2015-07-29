using JJ.Business.CanonicalModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class AudioFileOutputChannelViewModel
    {
        public int ID { get; set; }

        public int IndexNumber { get; set; }

        public string Name { get; set; }

        /// <summary> nullable </summary>
        public IDAndName Outlet { get; set; }
    }
}
