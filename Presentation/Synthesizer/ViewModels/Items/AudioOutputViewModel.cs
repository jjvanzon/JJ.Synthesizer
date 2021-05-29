using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class AudioOutputViewModel
    {
        public int ID { get; set; }
        public int SamplingRate { get; set; }
        public IDAndName SpeakerSetup { get; set; }
        public int MaxConcurrentNotes { get; set; }
        public double DesiredBufferDuration { get; set; }
    }
}
