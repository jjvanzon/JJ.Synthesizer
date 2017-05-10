using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SamplePropertiesViewModel : ViewModelBase
    {
        public SampleViewModel Entity { get; set; }

        public int DocumentID { get; set; }

        public IList<IDAndName> AudioFileFormatLookup { get; set; }
        public IList<IDAndName> InterpolationTypeLookup { get; set; }
        public IList<IDAndName> SampleDataTypeLookup { get; set; }
        public IList<IDAndName> SpeakerSetupLookup { get; set; }
        internal int? OutletIDToPlay { get; set; }
    }
}
