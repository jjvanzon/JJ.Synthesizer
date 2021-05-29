using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioFileOutputPropertiesViewModel : ScreenViewModelBase
    {
        public AudioFileOutputViewModel Entity { get; set; }

        public IList<IDAndName> AudioFileFormatLookup { get; set; }
        public IList<IDAndName> SampleDataTypeLookup { get; set; }
        public IList<IDAndName> SpeakerSetupLookup { get; set; }

        // This property is not used, because AudioFileOutputProperties the user interface is not finished. You cannot even use it.
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IList<IDAndName> OutletLookup { get; set; }
    }
}
