using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentDeleteViewModel
    {
        public bool Visible { get; set; }
        public IDAndName Document { get; set; }
    }
}
