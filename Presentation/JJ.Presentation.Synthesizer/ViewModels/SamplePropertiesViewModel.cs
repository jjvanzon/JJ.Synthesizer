using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SamplePropertiesViewModel
    {
        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }

        public SampleViewModel Entity { get; set; }

        public IList<IDAndName> AudioFileFormats { get; set; }
        public IList<IDAndName> InterpolationTypes { get; set; }
        public IList<IDAndName> SampleDataTypes { get; set; }
        public IList<IDAndName> SpeakerSetups { get; set; }
    }
}
