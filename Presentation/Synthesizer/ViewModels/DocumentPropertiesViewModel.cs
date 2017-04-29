using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentPropertiesViewModel : ViewModelBase
    {
        public IDAndName Entity { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}
