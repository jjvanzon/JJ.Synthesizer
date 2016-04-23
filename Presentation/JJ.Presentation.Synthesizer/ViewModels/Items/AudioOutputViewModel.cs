using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class AudioOutputViewModel
    {
        public int ID { get; set; }
        public int SamplingRate { get; set; }
        public IDAndName SpeakerSetup { get; set; }
        public int MaxConcurrentNotes { get; set; }
        public double BufferDuration { get; set; }
    }
}
