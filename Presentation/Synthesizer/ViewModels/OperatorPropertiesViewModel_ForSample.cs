using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForSample : OperatorPropertiesViewModelBase
    {
        /// <summary>
        /// The lookup is inside the DocumentViewModel,
        /// to prevent a lot of repeated data. So use the lookup from there.
        /// </summary>
        public SampleViewModel Sample { get; set; }

        public IList<IDAndName> AudioFileFormatLookup { get; set; }
        public IList<IDAndName> InterpolationTypeLookup { get; set; }
        public IList<IDAndName> SampleDataTypeLookup { get; set; }
        public IList<IDAndName> SpeakerSetupLookup { get; set; }
    }
}
