using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentPropertiesViewModel : ScreenViewModelBase
    {
        public IDAndName Entity { get; set; }
        internal int? OutletIDToPlay { get; set; }
    }
}
