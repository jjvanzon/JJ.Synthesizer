using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioOutputPropertiesViewModel : ViewModelBase
    {
        public AudioOutputViewModel Entity { get; set; }
        public IList<IDAndName> SpeakerSetupLookup { get; set; }
        internal int? OutletIDToPlay { get; set; }
    }
}
