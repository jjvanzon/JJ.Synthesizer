using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class MidiMappingGroupDetailsViewModel : ScreenViewModelBase
    {
        public IDAndName MidiMappingGroup { get; set; }
        public Dictionary<int, MidiMappingItemViewModel> MidiMappings { get; set; }
        public MidiMappingItemViewModel SelectedMidiMapping { get; set; }
        public int CreatedMidiMappingID { get; set; }
    }
}
