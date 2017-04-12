using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class LibraryTreeNodeViewModel
    {
        public int LowerDocumentReferenceID { get; set; }
        public string Caption { get; set; }
        public IList<IDAndName> Patches { get; set; }
    }
}
