using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public sealed class AudioOutputPropertiesViewModel : ViewModelBase
    {
        public AudioOutputViewModel Entity { get; set; }
        public IList<IDAndName> SpeakerSetupLookup { get; set; }
        internal int? OutletIDToPlay { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
