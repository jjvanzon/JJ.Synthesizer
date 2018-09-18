namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class TopButtonBarViewModel : ScreenViewModelBase
    {
        public bool CanAddToInstrument { get; set; }
        public bool CanCreate { get; set; }
        public bool CanOpenExternally { get; set; }
        public bool CanPlay { get; set; }
        public bool CanDelete { get; set; }
        public bool CanClone { get; set; }
    }
}