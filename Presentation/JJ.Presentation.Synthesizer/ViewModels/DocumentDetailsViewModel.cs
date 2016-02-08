using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentDetailsViewModel : ViewModelBase
    {
        public IDAndName Document { get; set; }
        public bool CanDelete { get; set; }
        public bool IDVisible { get; set; }
    }
}
