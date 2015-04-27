using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioFileOutputDetailsViewModel
    {
        public AudioFileOutputViewModel AudioFileOutput { get; set; }

        public IList<IDName> AudioFileFormats { get; set; }
        public IList<IDName> SampleDataTypes { get; set; }
        public IList<IDName> SpeakerSetups { get; set; }

        public IList<IDName> OutletLookup { get; set; }
    }
}
