using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class AudioFileOutputChannelViewModel
    {
        public AudioFileOutputChannelKeysViewModel Keys { get; set; }

        public string Name { get; set; }

        /// <summary> nullable </summary>
        public IDAndName Outlet { get; set; }
    }
}
