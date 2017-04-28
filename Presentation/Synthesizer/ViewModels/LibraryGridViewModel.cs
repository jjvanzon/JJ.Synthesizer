using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class LibraryGridViewModel : ViewModelBase
    {
        public int HigherDocumentID { get; set; }
        public IList<LibraryListItemViewModel> List { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}