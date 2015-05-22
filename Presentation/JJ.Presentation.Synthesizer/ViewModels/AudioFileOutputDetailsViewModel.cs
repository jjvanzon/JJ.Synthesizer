using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioFileOutputPropertiesViewModel
    {
        public bool Visible { get; set; }

        public AudioFileOutputViewModel AudioFileOutput { get; set; }

        public IList<IDAndName> AudioFileFormats { get; set; }
        public IList<IDAndName> SampleDataTypes { get; set; }
        public IList<IDAndName> SpeakerSetups { get; set; }

        public IList<IDAndName> OutletLookup { get; set; }
    }
}
