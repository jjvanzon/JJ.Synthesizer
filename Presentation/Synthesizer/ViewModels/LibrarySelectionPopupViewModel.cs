using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class LibrarySelectionPopupViewModel : ViewModelBase
    {
        public int HigherDocumentID { get; set; }
        public IList<IDAndName> List { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}
