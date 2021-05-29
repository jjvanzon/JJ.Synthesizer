using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class MonitoringBarMidiViewModel
    {
        public MonitoringItemViewModel NoteNumber { get; set; }
        public MonitoringItemViewModel Velocity { get; set; }
        public MonitoringItemViewModel Controller { get; set; }
        public MonitoringItemViewModel Channel { get; set; }
        public bool IsEmpty { get; set; }
    }
}