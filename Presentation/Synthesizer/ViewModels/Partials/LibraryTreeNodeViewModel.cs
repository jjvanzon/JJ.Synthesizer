using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class LibraryTreeNodeViewModel
    {
        public int LowerDocumentReferenceID { get; set; }
        public string Caption { get; set; }
        public IList<IDAndName> Patches { get; set; }
    }
}
