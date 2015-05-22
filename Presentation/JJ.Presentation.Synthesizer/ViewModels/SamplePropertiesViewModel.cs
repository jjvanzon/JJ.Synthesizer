using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SamplePropertiesViewModel
    {
        public bool Visible { get; set; }

        public SampleViewModel Sample { get; set; }

        public IList<IDAndName> AudioFileFormats { get; set; }
        public IList<IDAndName> InterpolationTypes { get; set; }
        public IList<IDAndName> SampleDataTypes { get; set; }
        public IList<IDAndName> SpeakerSetups { get; set; }
    }
}
