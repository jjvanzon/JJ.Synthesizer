using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class MonitoringBarViewModel : ScreenViewModelBase
    {
        public MonitoringBarMidiViewModel Midi { get; set; }
        public MonitoringBarSynthViewModel Synth { get; set; }
    }
}